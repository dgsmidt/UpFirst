using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Controllers
{
    //[Produces("application/json")]
    public class SalaAulasController : Controller
    {
        private readonly UpFirstDbContext _dbContext;
        public SalaAulasController(UpFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int alunoId, int? cursoId)
        {
            SalaAulaVM salaAulaVM = new SalaAulaVM();

            Matricula matricula = _dbContext.Matriculas.Where(m => m.CursoId == cursoId && m.AlunoId == alunoId).SingleOrDefault();

            Curso curso = _dbContext.Cursos.Find(cursoId);

            IEnumerable<Modulo> modulos = _dbContext.Modulos
                .OrderBy(m => m.NumeroModulo)
                .Where(m => m.CursoId == cursoId).ToList();

            salaAulaVM.Curso = curso;

            foreach (var modulo in salaAulaVM.Curso.Modulos)
            {
                IEnumerable<Aula> aulas = _dbContext.Aulas.OrderBy(a => a.NumeroAula).Where(a => a.ModuloId == modulo.Id).ToList();
            }

            StatusAulas statusAulas = _dbContext.StatusAulas
                .Include(sa => sa.Matricula.Aluno)
                .Include(sa => sa.AulasAssistidas)
                .Where(sa => sa.MatriculaId == matricula.Id)
                .SingleOrDefault();

            if (statusAulas == null)
            {
                int aulaAssistindoId = salaAulaVM.Curso.Modulos[0].Aulas[0].Id;
                int aulaPodeMarcarAssistidaId = aulaAssistindoId;
                int ultimoModuloLiberado = salaAulaVM.Curso.Modulos[0].Id;

                StatusAulas sa = new StatusAulas
                {
                    MatriculaId = matricula.Id,
                    AulaAssistindoId = aulaAssistindoId,
                    AulaPodeMarcarAssistidaId = aulaPodeMarcarAssistidaId,
                    UltimoModuloLiberadoId = ultimoModuloLiberado,
                    AulasAssistidas = new List<AulaAssistida>()
                };

                _dbContext.StatusAulas.Add(sa);
                _dbContext.SaveChanges();

                salaAulaVM.StatusAulas = sa;
            }
            else
            {
                salaAulaVM.StatusAulas = statusAulas;
            }

            foreach (var modulo in salaAulaVM.Curso.Modulos)
            {
                Modulo m = _dbContext.Modulos.Find(salaAulaVM.StatusAulas.UltimoModuloLiberadoId);

                if (m.NumeroModulo >= modulo.NumeroModulo)
                {
                    modulo.Liberado = true;

                    if (salaAulaVM.StatusAulas.AvaliacaoLiberadaId == modulo.AvaliacaoId)
                        modulo.AvaliacaoLiberada = true;

                    foreach (var aula in modulo.Aulas)
                    {
                        AulaAssistida aulaAssistida = salaAulaVM.StatusAulas.AulasAssistidas.FirstOrDefault(aa => aa.AulaId == aula.Id);

                        if (aulaAssistida != null) aula.Assistida = true;

                        if (aula.Id == salaAulaVM.StatusAulas.AulaAssistindoId)
                        {
                            aula.Assistindo = true;

                            AnotacaoAula anotacaoAula = _dbContext.AnotacoesAulas
                                .Where(aa => aa.AulaId == aula.Id && aa.AlunoId == salaAulaVM.StatusAulas.Matricula.AlunoId)
                                .FirstOrDefault();

                            if (anotacaoAula != null)
                                aula.Anotacoes = anotacaoAula.Anotacao;
                        }

                        if (aula.Id == salaAulaVM.StatusAulas.AulaPodeMarcarAssistidaId) aula.PodeMarcarAssistida = true;
                    }
                }
            }

            return View(salaAulaVM);
        }
        [HttpPost]
        public IActionResult PostAnotacoes(int aulaId, int alunoId, string anotacoes)
        {
            AnotacaoAula anotacaoAula = _dbContext.AnotacoesAulas
                .Where(aa => aa.AlunoId == alunoId)
                .Where(aa => aa.AulaId == aulaId)
                .SingleOrDefault();

            if (anotacaoAula == null)
            {
                _dbContext.AnotacoesAulas.Add(new AnotacaoAula { AlunoId = alunoId, AulaId = aulaId, Anotacao = anotacoes });
            }
            else
            {
                anotacaoAula.Anotacao = anotacoes;
                _dbContext.Update(anotacaoAula);
            }

            _dbContext.SaveChanges();

            return Ok();
        }
        public string GetAnotacoes(int aulaId, int alunoId)
        {
            var anotacoes = _dbContext.AnotacoesAulas
                .Where(a => a.AlunoId == alunoId && a.AulaId == aulaId)
                .Select(a => a.Anotacao)
                .SingleOrDefault();

            return (anotacoes ?? "");
        }
        public JsonResult GetStatusCheckAssistida(int aulaId, int matriculaId)
        {
            bool assistida = false;
            bool habilitar = false;

            StatusAulas sa = _dbContext.StatusAulas
                .Include(sa => sa.AulasAssistidas)
                .Where(sa => sa.MatriculaId == matriculaId)
                .SingleOrDefault();

            Matricula matricula = _dbContext.Matriculas.Find(matriculaId);

            if (sa.AulaPodeMarcarAssistidaId == aulaId)
                habilitar = true;

            AulaAssistida aa = sa.AulasAssistidas.Where(aa => aa.AulaId == aulaId && aa.AlunoId == matricula.AlunoId).FirstOrDefault();

            if (aa != null)
            {
                assistida = true;
            }

            return Json(new { habilitar = habilitar, assistida = assistida });
        }
        public JsonResult GetAvaliacaoEnabled(int aulaId, int matriculaId)
        {
            StatusAulas statusAulas = _dbContext.StatusAulas
                .Where(sa => sa.MatriculaId == matriculaId)
                .SingleOrDefault();

            if (statusAulas.AvaliacaoLiberadaId > 0)
            {
                return Json(new { enabled = true, nomeBotao = "#avaliacao_" + statusAulas.AvaliacaoLiberadaId.ToString() });
            }
            else
            {
                return Json(new { enabled = false, nomeBotao = "#avaliacao_0" });
            }
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot" + @"\uploads", filename.Replace("/uploads/", ""));

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filename.Replace("/uploads/", ""), out contentType);

            return File(memory, contentType, Path.GetFileName(path));
        }
        public string GetMaterialApoio(int aulaId)
        {
            var aula = _dbContext.Aulas.Find(aulaId);

            return (aula != null ? aula.MaterialApoio : "");
        }
        public JsonResult GetArquivosApoio(int aulaId)
        {
            var arquivosApoio = _dbContext.ArquivosApoio.Where(aa => aa.AulaId == aulaId).Select(aa => aa.FileName).ToList();

            return Json(arquivosApoio);
        }
        [HttpPost]
        public JsonResult UpdateAulaAssistida(int aulaId, int matriculaId)
        {
            string response = string.Empty;

            Matricula matricula = _dbContext.Matriculas.Find(matriculaId);

            StatusAulas statusAulas = _dbContext.StatusAulas
                .Include(sa => sa.AulasAssistidas)
                .Where(sa => sa.MatriculaId == matriculaId)
                .SingleOrDefault();

            statusAulas.AulasAssistidas.Add(new AulaAssistida { AulaId = aulaId, AlunoId = matricula.AlunoId });

            // Definir proxima aula que pode marcar assistida

            // Verificar se é a última aula do módulo
            Aula aula = _dbContext.Aulas.Find(aulaId);
            Modulo modulo = _dbContext.Modulos.Include(m => m.Aulas).Where(m => m.Id == aula.ModuloId).FirstOrDefault();

            // É última aula
            if (aula.NumeroAula == modulo.Aulas.Count)
            {
                statusAulas.AulaPodeMarcarAssistidaId = 0;

                // Habilitar avaliacao do modulo
                statusAulas.AvaliacaoLiberadaId = modulo.AvaliacaoId;

                // Se não houver avaliacao para o modulo, habilitar próximo módulo
                if (statusAulas.AvaliacaoLiberadaId == null)
                {
                    Curso curso = _dbContext.Cursos
                        .Include(c => c.Modulos)
                          .ThenInclude(m => m.Aulas)
                        .Where(c => c.Id == modulo.CursoId)
                        .FirstOrDefault();

                    Modulo novoModulo = curso.Modulos.Where(m => m.NumeroModulo == modulo.NumeroModulo + 1).FirstOrDefault();

                    // Se há modulo seguinte
                    if (novoModulo != null)
                    {
                        response = "reload";

                        statusAulas.UltimoModuloLiberadoId = novoModulo.Id;

                        Modulo m = _dbContext.Modulos
                            .Include(m => m.Aulas)
                            .Where(m => m.Id == novoModulo.Id)
                            .SingleOrDefault();

                        Aula a = m.Aulas.Where(a => a.NumeroAula == 1).SingleOrDefault();

                        statusAulas.AulaPodeMarcarAssistidaId = a.Id;
                        statusAulas.AulaAssistindoId = a.Id;
                    }
                    else
                    {
                        matricula.CursoConcluido = true;
                        response = "end";
                    }
                }
            }
            else // Não é a ultima aula
            {
                Aula a = modulo.Aulas.Where(a => a.NumeroAula == aula.NumeroAula + 1).SingleOrDefault();

                statusAulas.AulaPodeMarcarAssistidaId = a.Id;
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return Json(new { response = response });
        }
        [HttpPost]
        public IActionResult UpdateAssistindoAula(int aulaId, int matriculaId)
        {
            StatusAulas sa = _dbContext.StatusAulas.Where(sa => sa.MatriculaId == matriculaId).SingleOrDefault();

            sa.AulaAssistindoId = aulaId;

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
