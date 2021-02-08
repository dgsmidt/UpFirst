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
            var upFirstDbContext = _context.Pagamentos
                .Include(p => p.Matricula)
                    .ThenInclude(m => m.Aluno);

            return View(await upFirstDbContext.ToListAsync());
        }
        private void RegistrarPagamento(string forma, string externalReference, string orderId, long paymentId,
            string status, string paymentStatusDetail, decimal valor, string tipoPagamento)
        {
            ExternalReference extRef = new ExternalReference(externalReference);

            Pagamento pagamento = new Pagamento
            {
                //AlunoId = extRef.AlunoId,
                //CursoId = extRef.CursoId,
                Forma = forma,
                Data = DateTime.Now,
                OrderId = orderId,
                PaymentId = paymentId,
                Status = status,
                Valor = valor,
                StatusDetail = paymentStatusDetail,
                TipoPagamento = tipoPagamento
            };

            _context.Pagamentos.Add(pagamento);

            _context.SaveChanges();

            if (status == "approved")
            {
                RegistrarMatricula(extRef.AlunoId, extRef.CursoId, pagamento.Id);
            }
        }
        private void RegistrarMatricula(int alunoId, int cursoId, int pagamentoId)
        {
            Matricula m = _context.Matriculas.Where(m => m.CursoId == cursoId & m.AlunoId == alunoId).FirstOrDefault();

            if (m != null)
            {
                m.Liberada = true;
                _context.Matriculas.Update(m);
            }
            else
            {
                _context.Matriculas.Add(new Matricula { AlunoId = alunoId, CursoId = cursoId, Liberada = true, PagamentoId = pagamentoId, CursoConcluido = false });
            }

            _context.SaveChanges();
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
        // Ou para: https://escoladesucessofinanceiro.com/en/pagamentos/MercadoPagoWebHook
        // Teste no IIS LOCAL (Não funciona no IIS Express): http://Cyrex20.ddns.net:85/Web/en/pagamentos/MercadoPagoWebHook
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
