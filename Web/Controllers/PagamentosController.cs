using DAL;
using DAL.Models;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class PagamentosController : Controller
    {
        private IConfiguration Configuration { get; }
        private readonly UpFirstDbContext _context;
        public PagamentosController(UpFirstDbContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            _context = context;

            if (SDK.AccessToken == null)
                SDK.AccessToken = Configuration.GetSection("MercadoPago").GetSection("AccessToken").Value;

        }
        private class IPNContext
        {
            public Microsoft.AspNetCore.Http.HttpRequest IPNRequest { get; set; }
            public string RequestBody { get; set; }
            public string Verification { get; set; } = String.Empty;
        }
        public async Task<IActionResult> Index()
        {
            var upFirstDbContext = _context.Pagamentos.Include(p => p.Aluno).Include(p => p.Curso);
            return View(await upFirstDbContext.ToListAsync());
        }
        private void RegistrarPagamento(string forma, string externalReference, string orderId, long paymentId,
            string status, string paymentStatusDetail, decimal valor, string tipoPagamento)
        {
            ExternalReference extRef = new ExternalReference(externalReference);

            _context.Pagamentos.Add(new Pagamento
            {
                AlunoId = extRef.AlunoId,
                CursoId = extRef.CursoId,
                Forma = forma,
                Data = DateTime.Now,
                OrderId = orderId,
                PaymentId = paymentId,
                Status = status,
                Valor = valor,
                StatusDetail = paymentStatusDetail,
                TipoPagamento = tipoPagamento
            });

            if (status == "approved")
            {
                PreencherCursosAlunos(extRef.AlunoId, extRef.CursoId);
            }
        }
        private void PreencherCursosAlunos(int alunoId, int cursoId)
        {
            CursoAluno ca = _context.CursosAlunos.Where(ca => ca.CursoId == cursoId & ca.AlunoId == alunoId).FirstOrDefault();

            if (ca != null)
            {
                ca.Liberado = true;
                _context.CursosAlunos.Update(ca);
            }
            else
            {
                _context.CursosAlunos.Add(new CursoAluno { AlunoId = alunoId, CursoId = cursoId, Liberado = true, Data = DateTime.Now });

                PreencherModulosAlunos(alunoId, cursoId);
                PreencherAulasAlunos(alunoId, cursoId);
            }

            _context.SaveChanges();
        }
        private void PreencherModulosAlunos(int alunoId, int cursoId)
        {
            bool liberado;

            Curso curso = _context.Cursos
                .Include(c => c.Modulos)
                .Where(c => c.Id == cursoId)
                .SingleOrDefault();

            foreach (var modulo in curso.Modulos)
            {
                if (modulo.NumeroModulo == 1)
                    liberado = true;
                else
                    liberado = false;

                _context.ModulosAlunos.Add(new ModuloAluno { AlunoId = alunoId, ModuloId = modulo.Id, Liberado = liberado, NumeroModulo = modulo.NumeroModulo });
            }

            _context.SaveChanges();
        }
        private void PreencherAulasAlunos(int alunoId, int cursoId)
        {
            Curso curso = _context.Cursos
                .Include(c => c.Modulos)
                    .ThenInclude(m => m.Aulas)
                .Where(c => c.Id == cursoId)
                .SingleOrDefault();

            bool assistindo = false;
            bool habilitarAssistida = false;

            foreach (var modulo in curso.Modulos)
            {

                foreach (var aula in modulo.Aulas)
                {
                    // Assistindo a primeira aula
                    if (aula.NumeroAula == 1 && modulo.NumeroModulo == 1)
                    {
                        assistindo = true;
                        habilitarAssistida = true;
                    }
                    else
                    {
                        assistindo = false;
                        habilitarAssistida = false;
                    }

                    _context.AulasAlunos.Add(new AulaAluno
                    {
                        AulaId = aula.Id,
                        AlunoId = alunoId,
                        Assistindo = assistindo,
                        HabilitarAssistida = habilitarAssistida,
                        NumeroAula = aula.NumeroAula
                    });
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public IActionResult ParametrosTeste()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        // Instruções: https://www.mercadopago.com.br/developers/pt/guides/notifications/webhooks/
        // Configurar WebHook: https://www.mercadopago.com/mlb/account/webhooks
        // Ajustar para: https://upfirst.com.br/en/pagamentos/MercadoPagoWebHook
        // Teste no IIS local: http://Cyrex20.ddns.net:85/Web/us/pagamentos/MercadoPagoWebHook
        // As vezes não funciona no IIS Local, precisa reiniciar o roteador
        public IActionResult MercadoPagoWebHook(MercadoPagoData data, string type)
        {
            _context.MercadoPago_WebHooks.Add(new MercadoPago_WebHook { Data = DateTime.Now, DataId = data.id, Type = type });
            _context.SaveChanges();

            switch (type)
            {
                case "test":
                    break;

                case "payment":
                    try
                    {
                        Payment pay = Payment.FindById(data.id);

                        string tipoPagemento = string.Empty;

                        switch (pay.PaymentTypeId)
                        {
                            case PaymentTypeId.account_money:
                                tipoPagemento = "account_money";
                                break;
                            case PaymentTypeId.ticket:
                                tipoPagemento = "ticket";
                                break;
                            case PaymentTypeId.bank_transfer:
                                tipoPagemento = "bank_transfer";
                                break;
                            case PaymentTypeId.atm:
                                tipoPagemento = "atm";
                                break;
                            case PaymentTypeId.credit_card:
                                tipoPagemento = "credit_card";
                                break;
                            case PaymentTypeId.debit_card:
                                tipoPagemento = "debit_card";
                                break;
                            case PaymentTypeId.prepaid_card:
                                tipoPagemento = "prepaid_card";
                                break;
                            case PaymentTypeId.digital_currency:
                                tipoPagemento = "account_money";
                                break;
                            default:
                                break;
                        }

                        RegistrarPagamento(
                            "Mercado Pago",
                            pay.ExternalReference,
                            pay.Order.Value.Id.ToString(),
                            long.Parse(pay.Id.ToString()),
                            pay.Status.Value.ToString(),
                            pay.StatusDetail,
                            Decimal.Parse(pay.TransactionAmount.ToString()),
                            tipoPagemento
                            );
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    break;

                default:
                    break;
            }

            return Ok();
        }
        //[AllowAnonymous]
        //[HttpPost]
        // Instruções: https://www.mercadopago.com.br/developers/pt/guides/notifications/ipn/
        // Para ajustar o IPN no MP: https://www.mercadopago.com.br/ipn-notifications
        // Ajustar para: https://upfirst.com.br/en/pagamentos/mercadopagoIpn
        // Para teste no IIS local: http://cyrex20.ddns.net:85/web/en/pagamentos/mercadopagoIpn
        //public async Task<IActionResult> MercadoPagoIpn(string topic, long id)
        //{
        //    await _context.MercadoPago_Ipns.AddAsync(new MercadoPago_Ipn { Data = DateTime.Now, Topic = topic, Id = id });
        //    await _context.SaveChangesAsync();

        //    // Acessar a API para obter a informação completa em:
        //    // https://www.mercadopago.com.br/developers/en/reference/payments/_payments_id/get/

        //    switch (topic)
        //    {
        //        case "merchant_order":
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                using (var response = await httpClient.GetAsync("https://api.mercadopago.com/v1/merchant_orders/" + id + "?access_token=" + SDK.GetAccessToken()))
        //                {
        //                    string apiResponse = await response.Content.ReadAsStringAsync();
        //                }
        //            }
        //            break;
        //        case "payment":
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                using (var response = await httpClient.GetAsync("https://api.mercadopago.com/v1/payments/" + id + "?access_token=" + SDK.GetAccessToken()))
        //                {
        //                    string apiResponse = await response.Content.ReadAsStringAsync();
        //                }
        //            }
        //            break;
        //        case "chargebacks":
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                using (var response = await httpClient.GetAsync("https://api.mercadopago.com/v1/chargebacks/" + id + "?access_token=" + SDK.GetAccessToken()))
        //                {
        //                    string apiResponse = await response.Content.ReadAsStringAsync();
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }

        //    return Ok();
        //}
    }
}
