using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using MercadoPago.Resources;
using MercadoPago;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly UpFirstDbContext _context;
        public PagamentosController(UpFirstDbContext context)
        {
            _context = context;
        }
        private class IPNContext
        {
            public Microsoft.AspNetCore.Http.HttpRequest IPNRequest { get; set; }
            public string RequestBody { get; set; }
            public string Verification { get; set; } = String.Empty;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        // Instruções: https://www.mercadopago.com.br/developers/pt/guides/notifications/ipn/
        // Instruções da Nova versão a partir de 9/12/19: https://www.mercadopago.com.ar/developers/pt/guides/changelog/migration-guides/ipn-ow-guide
        // Para ajustar o IPN no MP: https://www.mercadopago.com.br/ipn-notifications
        // Para teste no IIS local: http://cyrex20.ddns.net:85/web/en/pagamentos/mercadopagoIpn
        public async Task<IActionResult> MercadoPagoIpn(string topic, long id)
        {
            await _context.MercadoPago_Ipns.AddAsync(new MercadoPago_Ipn { Data = DateTime.Now, Topic = topic, Id = id });
            await _context.SaveChangesAsync();

            // Acessar a API para obter a informação completa em:
            // https://www.mercadopago.com.br/developers/en/reference/payments/_payments_id/get/

            SDK sdk = new SDK();

            // Configura credenciais
            sdk.AccessToken = "TEST-6467456385943763-071019-80668c976238c969667630c1bfe3673b-450980286";

            try
            {
                Payment pay = Payment.FindById(id, sdk);

                //using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                //{
                //    using (var response = await httpClient.GetAsync("https://api.mercadopago.com/v1/payments/" + id + "?access_token=" + SDK.GetAccessToken()))
                //    {
                //        string apiResponse = await response.Content.ReadAsStringAsync();
                //        await _context.Logs.AddAsync(new Log { Descricao = "Api response: " + apiResponse });
                //        await _context.SaveChangesAsync();
                //    }
                //}

                //pay.Save();

                //RegistrarPagamento("Mercado Pago", pay.ExternalReference, pay.Order.Value.Id1.ToString(), pay.Status.Value.ToString(), pay.TransactionAmount.ToString());

            }
            catch (Exception)
            {

                //throw;
            }

            return Ok();
        }

        private void RegistrarPagamento(string forma, string externalReference, string operacao, string newStatus, string valor)
        {
            // Procurar Plano pelo numero do contrato
            // Os tres ultimos digitos sao o numero do pagamento
            var ext = new ExternalReference(externalReference);
            int alunoId = ext.AlunoId;
            var cursoId = ext.CursoId;
        }
    }
}
