using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using EllipticCurve;
using LazZiya.ExpressLocalization;
using LazZiya.TagHelpers.Alerts;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using ViaCep;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly UpFirstDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IViaCepClient _viaCepClient;
        private readonly IHttpContextAccessor _httpContext;

        private readonly string culture;

        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContext,
            ISharedCultureLocalizer loc,
            UpFirstDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IViaCepClient viaCepClient)
        {
            _logger = logger;
            _loc = loc;
            _dbContext = dbContext;
            _userManager = userManager;
            _viaCepClient = viaCepClient;
            _httpContext = httpContext;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;


        }
        private class resp
        {
            public string code { get; set; }
            public string date { get; set; }
        };
        public string GetCheckoutMercadoPago(string titulo, decimal preco, int alunoId, int cursoId)
        {
            SDK sdk = new SDK();

            // Configura credenciais
            sdk.AccessToken = "TEST-1920092947670068-091015-1a0930cbd0faab1032667bb1ce5ec106-643459209";

            // Cria um objeto de preferência
            Preference preference = new Preference(sdk);

            // Cria um item na preferência
            preference.Items.Add(
              new Item()
              {
                  Title = titulo,
                  Quantity = 1,
                  CurrencyId = CurrencyId.BRL,
                  UnitPrice = (decimal)preco
              }
            );

            //var request = _httpContext.HttpContext.Request;
            //var basePath = request.Host + request.PathBase;

            //preference.BackUrls = new BackUrls()
            //{
            //    Success = basePath + "/Pagamentos/MercadoPagoSuccess",
            //    Failure = basePath + "/Pagamentos/MercadoPagoFailure",
            //    Pending = basePath + "/Pagamentos/MercadoPagoPending"
            //};

            preference.ExternalReference = "A" + alunoId + "C" + cursoId;
            preference.Save();

            //ViewData["Code"] = preference.Id;

            return preference.Id;
        }
        [HttpPost]
        public IActionResult Processar_Pagamento([Bind("preference_id,external_reference,merchant_order_id,payment_id,payment_status,payment_status_detail,processing_mode")] RetornoPagamentoMP retornoMp)
        {
            switch (retornoMp.payment_status)
            {
                case "approved":
                    ExternalReference eReference = new ExternalReference(retornoMp.external_reference);
                    CursosAlunos ca = new CursosAlunos { AlunoId = eReference.AlunoId, CursoId = eReference.CursoId , Liberado = true};

                    _dbContext.CursosAlunos.Add(ca);
                    _dbContext.SaveChanges();
                    break;

                default:
                    break;
            }

            return View(retornoMp);
        }
        [EnableCors]
        public async Task<IActionResult> GetCheckoutPagSeguro()
        {
            var r = new resp();

            var data = new
            {
                email = "daniel.smidt@yahoo.com.br",
                token = "9925EFC7E5574E0281BE0E0CA865C733",
                currency = "BRL",
                itemId1 = "001",
                itemDescription1 = "Curso UpFirst",
                itemAmount1 = "169.90",
                itemQuantity1 = "1",
                reference = "124665c23f7896adff508377925",
                senderName = "Natalie",
                senderAreaCode = "51",
                senderPhone = "991504360",
                senderEmail = "c81901614069859047077@sandbox.pagseguro.com.br"
            };
            using (var httpClient = new HttpClient())
            {
                var data1 = JsonConvert.SerializeObject(data);
                var requestBody = new StringContent(data1, Encoding.UTF8, "application/x-www-form-urlencoded");

                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                using (var response = await httpClient.PostAsync("https://ws.sandbox.pagseguro.uol.com.br/v2/checkout", requestBody))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    try
                    {
                        r = JsonConvert.DeserializeObject<resp>(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        ViewData["Code"] = response;
                        return View();
                        //throw;
                    }

                }
            }

            ViewData["Code"] = r.code;

            return View("GetCheckout");
        }
        public IActionResult Aula()
        {
            return Redirect("~/Aula.html");
        }
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public async Task<IActionResult> Index()
        {
            var checkoutIds = new List<String>();
            QuestionarioVM model;
            Aluno aluno;

            ViewData["AlunoId"] = 0;

            var request = _httpContext.HttpContext.Request;
            var basePath = request.PathBase;

            ViewData["PathProcessarPagamento"] = basePath + "/en/home/processar_pagamento";

            var cursos = await _dbContext.Cursos.ToListAsync();

            foreach (var item in cursos)
            {
                checkoutIds.Add(GetCheckoutMercadoPago("Curso UpFirst: " + RemoveDiacritics(item.Nome), item.Preco, 1, 1));
            }

            var configs = await _dbContext.Configuracoes.FirstOrDefaultAsync();

            ViewData["CabecalhoTexto1"] = configs.CabecalhoTexto1_Index;
            ViewData["Texto1"] = configs.Texto1_Index;
            ViewData["CheckoutIds"] = checkoutIds;

            if (User.Identity.IsAuthenticated)
            {
                aluno = _dbContext.Alunos.Where(q => q.UserId == _userManager.GetUserId(User)).SingleOrDefault();

                if (aluno != null)
                {
                    //ViewData["AlunoId"] = aluno.Id;

                    if (aluno.NotaQuestionario == 0)
                    {
                        model = new QuestionarioVM { Questionario = new Questionario { Perguntas = await _dbContext.PerguntasQuestionario.ToListAsync() }, AlunoId = aluno.Id };

                        return View("Questionario", model);
                    }
                }
            }

            return View(cursos);
            //return Redirect("~/Index.html");
        }
        public async Task<IActionResult> TestarQuestionario(int questionarioId, int alunoId)
        {
            var questionario = await _dbContext.Questionarios
                .Where(q => q.Id == questionarioId)
                .Include(q => q.Perguntas)
                .SingleOrDefaultAsync();

            //ViewData["AlunoId"] = alunoId;

            QuestionarioVM model = new QuestionarioVM { Questionario = questionario, AlunoId = alunoId };

            return View("Questionario", model);
        }
        public IActionResult Privacy()
        {
            // This is a sample to show how to localize 
            // custom messages from the backend.
            // The texts must be defined in ViewsLocalizationResource.xx.resx
            var msg = _loc.GetLocalizedString(culture, "Privacy Policy");

            // Use AlertTagHelper to show messages
            // Available options : .Success .Warning .Danger .Info .Dark .Light .Primary .Secondary
            // For more details visit: http://demo.ziyad.info/alert
            TempData.Warning(msg);

            return View();
        }
        public async Task<IActionResult> SalaCurso(int cursoId, int alunoId)
        {
            return RedirectToAction("Index", "SalaAulas", new { cursoId = cursoId, alunoId = alunoId });
        }
        public IActionResult Avaliacao(int alunoId, int avaliacaoId)
        {
            var avaliacao = _dbContext.Avaliacoes
                .Where(a => a.Id == avaliacaoId)
                .Include(a => a.Perguntas)
                    .ThenInclude(p => p.Respostas)
                 .Include(a => a.Modulo)
                    .ThenInclude(m => m.Curso)
                .FirstOrDefault();

            ViewData["AlunoId"] = alunoId;

            if (avaliacao == null)
            {
                return View("Error", new ErrorViewModel { RequestId = "Não há avaliações." });
            }
            else
            {
                return View(avaliacao);
            }
        }
        [HttpPost]
        public IActionResult UpdateNota(int alunoId, int moduloId, decimal valor)
        {
            var nota = _dbContext.Notas
                .Where(n => n.AlunoId == alunoId)
                .Where(n => n.ModuloId == moduloId)
                .FirstOrDefault();

            if (nota == null)
            {
                _dbContext.Notas.Add(new Nota { ModuloId = moduloId, AlunoId = alunoId, Valor = valor });
            }
            else
            {
                nota.Valor = valor;
                _dbContext.Notas.Update(nota);
            }

            _dbContext.SaveChanges();

            return Ok();
        }
        public async Task<IActionResult> SelecaoAvaliacao()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var aluno = await _dbContext.Alunos.Where(a => a.UserId == userId).FirstOrDefaultAsync();

            List<int> modulosId = new List<int>();
            ViewData["AlunoId"] = aluno.Id;

            var notas = _dbContext.Notas
                .Where(n => n.AlunoId == aluno.Id)
                    .Include(n => n.Modulo)
                .ToList();

            List<Modulo> modulosComNota = new List<Modulo>();

            foreach (var item in notas)
            {
                modulosComNota.Add(item.Modulo);
            }

            // Cursos que o aluno tem acesso
            var cursosAlunos = await _dbContext.CursosAlunos
                .Where(ca => ca.AlunoId == aluno.Id && ca.Liberado)
                .Include(ca => ca.Curso)
                    .ThenInclude(c => c.Modulos)
                .ToListAsync();

            // Modulos que tem acesso
            List<Modulo> modulos = new List<Modulo>();
            foreach (var item in cursosAlunos)
            {
                foreach (var modulo in item.Curso.Modulos)
                {
                    if (!modulosComNota.Contains(modulo))
                    {
                        modulos.Add(modulo);
                    }
                }
            }

            var model = _dbContext.Avaliacoes
                .Where(a => modulos.Contains(a.Modulo))
                .ToList();


            return View(model);
        }
        public async Task<IActionResult> SelecaoCurso()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var aluno = await _dbContext.Alunos.Where(a => a.UserId == userId).FirstOrDefaultAsync();

            ViewData["AlunoId"] = aluno.Id;

            var cursosAlunos = await _dbContext.CursosAlunos
                .Where(ca => ca.AlunoId == aluno.Id && ca.Liberado)
                .Include(ca => ca.Curso)
                .ToListAsync();

            List<Curso> cursos = new List<Curso>();

            if (cursosAlunos != null)
            {
                foreach (var item in cursosAlunos)
                {
                    cursos.Add(item.Curso);
                }
            }

            return View(cursos);
        }
        [HttpPost]
        public async Task<IActionResult> Questionario(QuestionarioVM model)
        {
            double media = 0;

            if (ModelState.IsValid)
            {
                media = CalcularMedia(model.Questionario.Perguntas);
                //var aluno = _dbContext.Alunos.Where(q => q.UserId == _userManager.GetUserId(User)).SingleOrDefault();
                var aluno = _dbContext.Alunos.Where(a => a.Id == model.AlunoId).SingleOrDefault();
                aluno.NotaQuestionario = (decimal)media;

                // Liberar todos os cursos
                var cursos = await _dbContext.Cursos.ToListAsync();

                if (cursos != null)
                {
                    foreach (var item in cursos)
                    {
                        await _dbContext.CursosAlunos.AddAsync(new CursosAlunos { AlunoId = model.AlunoId, CursoId = item.Id, Liberado = true });
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            //return View("Index");
            ViewData["AlunoId"] = model.AlunoId;
            return View("Media", media);
        }
        private double CalcularMedia(List<PerguntaQuestionario> perguntas)
        {
            float soma = 0;

            foreach (var item in perguntas)
            {
                soma += int.Parse(item.Resposta);
            }

            return Math.Round(soma / (float)perguntas.Count(), 2);
        }

        public IActionResult GetCEPInfo()
        {
            var model = new ViaCepResult();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetCEPInfo(ViaCepResult model)
        {
            CancellationToken canc = new CancellationToken();
            var result = await _viaCepClient.SearchAsync(model.ZipCode, canc);

            return View(result);
        }

        public IActionResult Planos()
        {
            return Redirect("~/planos.html");
        }
        public async Task<IActionResult> Evolucao(int alunoId)
        {
            var model = await _dbContext.Notas
                .Where(a => a.Id == alunoId)
                .Include(a => a.Modulo)
                .ToListAsync();

            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
