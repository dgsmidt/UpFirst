using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using LazZiya.ExpressLocalization;
using LazZiya.TagHelpers.Alerts;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IEmailSender _emailSender;
        private IConfiguration Configuration { get; }

        private readonly string culture;

        public HomeController(ILogger<HomeController> logger,
            IHttpContextAccessor httpContext,
            ISharedCultureLocalizer loc,
            UpFirstDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IViaCepClient viaCepClient,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            Configuration = configuration;
            _logger = logger;
            _loc = loc;
            _dbContext = dbContext;
            _userManager = userManager;
            _viaCepClient = viaCepClient;
            _httpContext = httpContext;
            _emailSender = emailSender;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;

            if (SDK.AccessToken == null)
                SDK.AccessToken = Configuration.GetSection("MercadoPago").GetSection("AccessToken").Value;
        }
        //private class resp
        //{
        //    public string code { get; set; }
        //    public string date { get; set; }
        //};
        public string GetCheckoutMercadoPago(string titulo, decimal preco, int alunoId, int cursoId)
        {

            // Cria um objeto de preferência
            Preference preference = new Preference();

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
        public IActionResult Processar_PagamentoMP([Bind("preference_id,external_reference,merchant_order_id,payment_id,payment_status,payment_status_detail,processing_mode")] RetornoPagamentoMP retornoMp)
        {
            switch (retornoMp.payment_status)
            {
                case "approved":
                    retornoMp.Descricao = "Payment Approved";
                    break;
                case "pending":
                    retornoMp.Descricao = "Payment in process";
                    break;
                case "in_process":
                    retornoMp.Descricao = "Payment in process";
                    break;
                case "rejected":
                    retornoMp.Descricao = "Payment Rejected";
                    break;
                default:
                    break;
            }

            return View(retornoMp);
        }
        //[EnableCors]
        //public async Task<IActionResult> GetCheckoutPagSeguro()
        //{
        //    var r = new resp();

        //    var data = new
        //    {
        //        email = "daniel.smidt@yahoo.com.br",
        //        token = "9925EFC7E5574E0281BE0E0CA865C733",
        //        currency = "BRL",
        //        itemId1 = "001",
        //        itemDescription1 = "Curso UpFirst",
        //        itemAmount1 = "169.90",
        //        itemQuantity1 = "1",
        //        reference = "124665c23f7896adff508377925",
        //        senderName = "Natalie",
        //        senderAreaCode = "51",
        //        senderPhone = "991504360",
        //        senderEmail = "c81901614069859047077@sandbox.pagseguro.com.br"
        //    };
        //    using (var httpClient = new HttpClient())
        //    {
        //        var data1 = JsonConvert.SerializeObject(data);
        //        var requestBody = new StringContent(data1, Encoding.UTF8, "application/x-www-form-urlencoded");

        //        requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        //        using (var response = await httpClient.PostAsync("https://ws.sandbox.pagseguro.uol.com.br/v2/checkout", requestBody))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();

        //            try
        //            {
        //                r = JsonConvert.DeserializeObject<resp>(apiResponse);
        //            }
        //            catch (Exception ex)
        //            {
        //                ViewData["Code"] = response;
        //                return View();
        //                //throw;
        //            }

        //        }
        //    }

        //    ViewData["Code"] = r.code;

        //    return View("GetCheckout");
        //}
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
        [HttpPost]
        public async Task<IActionResult> Index(IndexVM modelo)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = modelo.InputModel.Email,
                    Email = modelo.InputModel.Email,
                    Nome = modelo.InputModel.Nome,
                    WhatsApp = modelo.InputModel.WhatsApp

                };

                var result = await _userManager.CreateAsync(user, modelo.InputModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Nome", user.Nome));

                    _logger.LogInformation("User created a new account with password.");

                    await _dbContext.Alunos.AddAsync(new Aluno { UserId = user.Id, Nome = user.Nome, Email = user.Email, WhatsApp = user.WhatsApp });
                    await _dbContext.SaveChangesAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        //values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        values: new { area = "Identity", userId = user.Id, code, culture },
                        protocol: Request.Scheme);

                    var mailHeader = _loc.GetLocalizedString(culture, "Confirm your email");
                    //var mailBody = _loc.GetLocalizedString(culture, "Please confirm your account by <a href='{0}'>clicking here</a>.", HtmlEncoder.Default.Encode(callbackUrl));
                    var mailBody = _loc.GetLocalizedString(culture, "Please confirm your account by <a href='{0}'>clicking here</a>.", callbackUrl);

                    //mailBody = mailBody.Replace("amp;amp;", "");

                    await _emailSender.SendEmailAsync(modelo.InputModel.Email, mailHeader, mailBody);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        return RedirectToPage("/Account/RegisterConfirmation", new { area = "Identity", email = modelo.InputModel.Email, culture });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    // model binding error already localized by ExpressLocalization
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            modelo.Cursos = _dbContext.Cursos.ToList();

            return View(modelo);
        }
        public async Task<IActionResult> Index()
        {
            var checkoutIds = new List<String>();
            QuestionarioVM model;
            Aluno aluno;

            ViewData["AlunoId"] = 0;

            var request = _httpContext.HttpContext.Request;
            var basePath = request.PathBase;

            ViewData["PathProcessarPagamentoMP"] = basePath + "/" + culture + "/home/processar_pagamentoMP";

            IndexVM viewModel = new IndexVM
            {
                InputModel = new InputModel(),

                Cursos = await _dbContext.Cursos.ToListAsync()
            };

            

            var configs = await _dbContext.Configuracoes.FirstOrDefaultAsync();

            ViewData["CabecalhoTexto1"] = configs.CabecalhoTexto1_Index;
            ViewData["Texto1"] = configs.Texto1_Index;

            if (User.Identity.IsAuthenticated)
            {
                string userName = _userManager.GetUserName(User);

                aluno = _dbContext.Alunos.Where(q => q.Email == userName).SingleOrDefault();

                if (aluno == null)
                {
                    ApplicationUser applicationUser = await _userManager.GetUserAsync(User);

                    if (applicationUser != null)
                    {
                        aluno = new Aluno { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), Nome = applicationUser.Nome, Email = User.FindFirstValue(ClaimTypes.Name), WhatsApp = applicationUser.WhatsApp };
                    }
                    else
                    {
                        aluno = new Aluno { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), Email = User.FindFirstValue(ClaimTypes.Name) };
                    }

                    _dbContext.Alunos.Add(aluno);

                    await _dbContext.SaveChangesAsync();
                }

                if (aluno != null)
                {
                    ViewData["AlunoId"] = aluno.Id;

                    foreach (var item in viewModel.Cursos)
                    {
                        checkoutIds.Add(GetCheckoutMercadoPago("Curso " + RemoveDiacritics(item.Nome), item.Preco, aluno.Id, item.Id));
                    }

                    ViewData["CheckoutIds"] = checkoutIds;

                    

                    viewModel.CheckoutIds = checkoutIds;

                    if (aluno.NotaQuestionario == 0)
                    {
                        model = new QuestionarioVM { Questionario = new Questionario { Perguntas = await _dbContext.PerguntasQuestionario.ToListAsync() }, AlunoId = aluno.Id };

                        return View("Questionario", model);
                    }
                }
            }
            else
            {
                viewModel.CheckoutIds = checkoutIds;

                foreach (var curso in viewModel.Cursos)
                {
                    viewModel.CheckoutIds.Add("");
                }
            }

            return View(viewModel);
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
        public IActionResult SalaCurso(int cursoId, int alunoId, string email)
        {
            if (email != null)
            {
                Aluno aluno = _dbContext.Alunos.Where(a => a.Email == email).SingleOrDefault();

                if (aluno != null)
                    alunoId = aluno.Id;
            }

            return RedirectToAction("Index", "SalaAulas", new { cursoId, alunoId });
        }
        public IActionResult Avaliacao(int alunoId, int avaliacaoId)
        {
            Avaliacao avaliacao = _dbContext.Avaliacoes
                .Where(a => a.Id == avaliacaoId)
                .Include(a => a.Perguntas)
                    .ThenInclude(p => p.Respostas)
                .Include(a => a.Modulo)
                    .ThenInclude(m => m.Aulas)
                .FirstOrDefault();

            List<AulaAluno> aa = _dbContext.AulasAlunos.Where(aa => aa.AlunoId == alunoId).ToList();

            if (avaliacao != null)
            {
                // Para cada aula do modulo
                foreach (var item in avaliacao.Modulo.Aulas)
                {
                    // Verificar se assistiu
                    if (!aa.Find(aa => aa.AulaId == item.Id).Assistida)
                    {
                        return View("DeveAssistirTodasAulas");
                    }
                }

                ViewData["AlunoId"] = alunoId;
                ViewData["ModuloId"] = avaliacao.ModuloId;
                ViewData["Cursoid"] = avaliacao.Modulo.CursoId;

                decimal notaCorte = _dbContext.Configuracoes
                    .Select(c => c.NotaDeCorte)
                    .FirstOrDefault();
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Não há avaliações." });
            }

            return View(avaliacao);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateNota(int alunoId, int moduloId, decimal valor)
        {
            bool aprovado = false;
            bool haMaisModulos = false;

            ModuloAluno moduloAluno = _dbContext.ModulosAlunos
                //.Include(ma => ma.Modulo)
                //    .ThenInclude(x => x.Aulas)
                .Where(ma => ma.AlunoId == alunoId && ma.ModuloId == moduloId)
                .SingleOrDefault();

            AulaAluno aulaAluno = _dbContext.AulasAlunos
                .Where(aa => aa.Assistindo && aa.AlunoId == alunoId)
                .SingleOrDefault();

            moduloAluno.Nota = valor;

            decimal notaCorte = _dbContext.Configuracoes
                .Select(c => c.NotaDeCorte)
                .FirstOrDefault();

            if (valor >= notaCorte) // Se nota >= nota de corte
            {
                aprovado = true;
                // Desabilitar avaliação e desligar flag de assistindo das aulas do módulo 
                if (aulaAluno != null)
                    aulaAluno.Assistindo = false;

                moduloAluno.AvaliacaoLiberada = false;

                // Liberar o proximo modulo
                ModuloAluno moduloLiberar = _dbContext.ModulosAlunos
                    .Include(ma => ma.AulasAlunos)
                    .Where(ma => ma.AlunoId == alunoId && ma.NumeroModulo == moduloAluno.NumeroModulo + 1)
                    .SingleOrDefault();

                if (moduloLiberar != null) // Há mais módulos no curso
                {
                    haMaisModulos = true;
                    moduloLiberar.Liberado = true;

                    // Ativar a primeira aula do novo módulo
                    Modulo modulo = _dbContext.Modulos
                        .Include(m => m.Aulas)
                        .Where(m => m.Id == moduloLiberar.ModuloId).SingleOrDefault();

                    foreach (var aula in modulo.Aulas)
                    {
                        AulaAluno aa = _dbContext.AulasAlunos
                                .Where(aa => aa.AulaId == aula.Id && aa.AlunoId == alunoId)
                                .SingleOrDefault();

                        aa.Assistida = false;

                        if (aula.NumeroAula == 1)
                        {
                            aa.HabilitarAssistida = true;
                            aa.Assistindo = true;
                        }

                        await _dbContext.SaveChangesAsync();
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return Json(new { aprovado, haMaisModulos });
        }
        //public async Task<IActionResult> SelecaoAvaliacao()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var userId = user?.Id;

        //    var aluno = await _dbContext.Alunos.Where(a => a.UserId == userId).FirstOrDefaultAsync();

        //    List<int> modulosId = new List<int>();
        //    ViewData["AlunoId"] = aluno.Id;

        //    var notas = _dbContext.Notas
        //        .Where(n => n.AlunoId == aluno.Id)
        //            .Include(n => n.Modulo)
        //        .ToList();

        //    List<Modulo> modulosComNota = new List<Modulo>();

        //    foreach (var item in notas)
        //    {
        //        modulosComNota.Add(item.Modulo);
        //    }

        //    // Cursos que o aluno tem acesso
        //    var cursosAlunos = await _dbContext.CursosAlunos
        //        .Where(ca => ca.AlunoId == aluno.Id && ca.Liberado)
        //        .Include(ca => ca.Curso)
        //            .ThenInclude(c => c.Modulos)
        //                .ThenInclude(m => m.Avaliacao)
        //        .ToListAsync();

        //    // Modulos que tem acesso
        //    List<Modulo> modulos = new List<Modulo>();

        //    foreach (var item in cursosAlunos)
        //    {
        //        foreach (var modulo in item.Curso.Modulos)
        //        {
        //            if (!modulosComNota.Contains(modulo))
        //            {
        //                modulos.Add(modulo);
        //            }
        //        }
        //    }

        //    List<Avaliacao> model = _dbContext.Avaliacoes
        //        .Where(a => modulos.Contains(a.Modulo))
        //        .ToList();


        //    return View(model);
        //}
        public async Task<IActionResult> SelecaoCurso()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var aluno = await _dbContext.Alunos.Where(a => a.UserId == userId).FirstOrDefaultAsync();

            ViewData["AlunoId"] = aluno.Id;

            var cursosAlunos = await _dbContext.CursosAlunos
                .Where(ca => ca.AlunoId == aluno.Id && ca.Liberado)
                .Include(ca => ca.Curso)
                .OrderBy(ca => ca.CursoId)
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
                //var cursos = await _dbContext.Cursos.ToListAsync();

                //if (cursos != null)
                //{
                //    foreach (var item in cursos)
                //    {
                //        await _dbContext.CursosAlunos.AddAsync(new CursosAlunos { AlunoId = model.AlunoId, CursoId = item.Id, Liberado = true });
                //    }
                //}

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
            if (User.Identity.IsAuthenticated)
            {
                Aluno aluno = _dbContext.Alunos
                    .Where(q => q.UserId == _userManager.GetUserId(User))
                    .SingleOrDefault();

                if (aluno != null) alunoId = aluno.Id;
            }

            List<ModuloAluno> modulosAluno = await _dbContext.ModulosAlunos
                .Include(ma => ma.Modulo)
                    .ThenInclude(m => m.Curso)
                .Where(ma => ma.AlunoId == alunoId).ToListAsync();


            return View(modulosAluno);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
