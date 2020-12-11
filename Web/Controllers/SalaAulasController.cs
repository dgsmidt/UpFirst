using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            CursoAluno cursoAluno;

            if (cursoId != null)
                cursoAluno = _dbContext.CursosAlunos.Where(ca => ca.CursoId == cursoId && ca.AlunoId == alunoId)
                    .Include(ca => ca.ModulosAlunos)
                    .Include(ca => ca.Curso)
                    .SingleOrDefault();
            else
                cursoAluno = _dbContext.CursosAlunos
                    .Include(ca => ca.Curso)
                    .FirstOrDefault();

            IEnumerable<Modulo> modulos = _dbContext.Modulos.OrderBy(m => m.NumeroModulo).Where(m => m.CursoId == cursoId).ToList();

            int i = 0;

            foreach (var modulo in modulos)
            {
                ModuloAluno ma = _dbContext.ModulosAlunos.Where(ma => ma.ModuloId == modulo.Id && ma.AlunoId == alunoId).SingleOrDefault();

                // Carregar somente modulos liberados
                if (ma.Liberado)
                {
                    cursoAluno.ModulosAlunos.Add(ma);

                    IEnumerable<Aula> aulas = _dbContext.Aulas.OrderBy(a => a.NumeroAula).Where(a => a.ModuloId == modulo.Id).ToList();

                    cursoAluno.ModulosAlunos[i].AulasAlunos = new List<AulaAluno>();

                    foreach (var aula in aulas)
                    {
                        AulaAluno aa = _dbContext.AulasAlunos.Where(aa => aa.AulaId == aula.Id && aa.AlunoId == alunoId).SingleOrDefault();

                        cursoAluno.ModulosAlunos[i].AulasAlunos.Add(aa);
                    }

                    i += 1;
                }
            }

            ViewData["AlunoId"] = alunoId;

            return View(cursoAluno);
        }
        [HttpPost]
        public IActionResult PostAnotacoes(int aulaId, int alunoId, string anotacoes)
        {
            var aulaAluno = _dbContext.AulasAlunos
                .Where(aa => aa.AlunoId == alunoId)
                .Where(aa => aa.AulaId == aulaId)
                .SingleOrDefault();

            if (aulaAluno == null)
            {
                _dbContext.AulasAlunos.Add(new AulaAluno { AlunoId = alunoId, AulaId = aulaId, Anotacoes = anotacoes });
            }
            else
            {
                aulaAluno.Anotacoes = anotacoes;
                _dbContext.Update(aulaAluno);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
        public string GetAnotacoes(int aulaId, int alunoId)
        {
            var anotacoes = _dbContext.AulasAlunos.
                Where(aa => aa.AlunoId == alunoId).
                Where(aa => aa.AulaId == aulaId).Select(aa => aa.Anotacoes)
                .SingleOrDefault();

            return (anotacoes ?? "");
        }
        public JsonResult GetStatusCheckAssistida(int aulaId, int alunoId)
        {
            AulaAluno aa = _dbContext.AulasAlunos
                .Where(aa => aa.AlunoId == alunoId && aa.AulaId == aulaId)
                .SingleOrDefault();

            return Json(new { habilitar = aa.HabilitarAssistida, assistida = aa.Assistida });
        }
        public JsonResult GetAvaliacaoEnabled(int aulaId, int alunoId)
        {

            //Aula a = _dbContext.Aulas
            //    .Include(a => a.Modulo)
            //    .Where(a => a.Id == aulaId)
            //    .SingleOrDefault();

            try
            {
                AulaAluno aa = _dbContext.AulasAlunos
                .Include(aa => aa.Aula)
                .Where(aa => aa.AlunoId == alunoId && aa.AulaId == aulaId)
                .SingleOrDefault();

                List<ModuloAluno> moduloAluno = _dbContext.ModulosAlunos
                .Where(ma => ma.ModuloId == aa.Aula.ModuloId && aa.AlunoId == alunoId)
                .ToList();

                int avaliacaoId = 0;
                bool avaliacaoLiberada = false;

                foreach (var item in moduloAluno)
                {
                    if (item.AlunoId == alunoId)
                    {
                        Modulo modulo = _dbContext.Modulos.Find(item.ModuloId);
                        avaliacaoId = modulo.AvaliacaoId ?? 0;
                        avaliacaoLiberada = item.AvaliacaoLiberada;
                    }
                }

                return Json(new { enabled = avaliacaoLiberada, nomeBotao = "#avaliacao_" + avaliacaoId.ToString() });
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            // Se houver aula não assistida, deasbilitar
            //foreach (var item in aa)
            //    if (!item.Assistida)
            //    {
            //        return Json(new { disabled = true, nomeBotao = "#avaliacao_" + a.Modulo.AvaliacaoId.ToString() });
            //    }

            // Senão, habilitar
            //moduloAluno.AvaliacaoLiberada = true;

            //_dbContext.Update(moduloAluno);
            //_dbContext.SaveChanges();


        }
        public string GetMaterialApoio(int aulaId)
        {
            var aula = _dbContext.Aulas.Find(aulaId);

            return (aula != null ? aula.MaterialApoio : "");
        }
        public JsonResult GetArquivosApoio(int aulaId)
        {
            var arquivosApoio = _dbContext.ArquivosApoio.Where(aa => aa.AulaId == aulaId).Select(aa=>aa.FileName).ToList();

            return  Json(arquivosApoio);
        }
        [HttpPost]
        public IActionResult UpdateAulaAssistida(int aulaId, int alunoId)
        {
            AulaAluno aulaAluno = _dbContext.AulasAlunos
                .Include(aa => aa.Aula)
                .Where(aa => aa.AlunoId == alunoId && aa.AulaId == aulaId)
                .SingleOrDefault();

            if (aulaAluno != null)
            {
                aulaAluno.Assistida = true;
                aulaAluno.HabilitarAssistida = false;
            }

            // Habilitar assistida para a proxima aula do modulo
            Aula aula = _dbContext.Aulas
                .Where(a => a.ModuloId == aulaAluno.Aula.ModuloId && a.NumeroAula == aulaAluno.Aula.NumeroAula + 1)
                .SingleOrDefault();

            // Se não assistiu todas
            if (aula != null)
            {
                AulaAluno aa = _dbContext.AulasAlunos
                    .Where(aa => aa.AlunoId == alunoId && aa.AulaId == aula.Id)
                    .SingleOrDefault();
                aa.HabilitarAssistida = true;
            }
            else // Habilitar avaliacao
            {
                AulaAluno aa = _dbContext.AulasAlunos
                    .Where(aa => aa.AlunoId == alunoId && aa.AulaId == aulaId)
                    .FirstOrDefault();

                ModuloAluno ma = _dbContext.ModulosAlunos
                    .Where(ma => ma.AlunoId == alunoId && ma.ModuloId == aa.Aula.ModuloId)
                    .SingleOrDefault();

                Avaliacao a = _dbContext.Avaliacoes
                    .Where(a => a.ModuloId == ma.ModuloId)
                    .SingleOrDefault();

                if (a != null) // Se há avaliacao cadastrada para o modulo
                    ma.AvaliacaoLiberada = true;
            }

            _dbContext.SaveChanges();

            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateAssistindoAula(int aulaId, int alunoId)
        {
            var aulasAluno = _dbContext.AulasAlunos
                .Include(aa => aa.Aula)
                .Where(aa => aa.AlunoId == alunoId)
                .ToList();

            foreach (var aulaAluno in aulasAluno)
            {
                if (aulaAluno.Assistindo)
                    aulaAluno.Assistindo = false;

                if (aulaAluno.AulaId == aulaId)
                    aulaAluno.Assistindo = true;
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
