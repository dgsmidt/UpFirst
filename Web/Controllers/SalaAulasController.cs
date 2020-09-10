using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class SalaAulasController : Controller
    {
        private readonly UpFirstDbContext _dbContext;
        public SalaAulasController(UpFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int alunoId, int? cursoId)
        {
            //var model = await _dbContext.Cursos
            //    .Include(c => c.Modulos)
            //        .ThenInclude(m => m.Aulas)
            //            .ThenInclude(a => a.AulasAlunos)
            //    .FirstOrDefaultAsync();

            Curso curso;

            if (cursoId != null)
            {
                curso = _dbContext.Cursos
                    .Where(c => c.Id == cursoId)
                .FirstOrDefault();
            }
            else
            {
                curso = _dbContext.Cursos
                .FirstOrDefault();
            }

            var cursoEntry = _dbContext.Entry(curso);

            // Preenche Modulos
            cursoEntry.Collection(c => c.Modulos)
                .Query()
                .OrderBy(m => m.NumeroModulo)
                .Load();

            // Preenche Aulas
            foreach (var modulo in curso.Modulos)
            {
                var moduleEntry = _dbContext.Entry(modulo);
                moduleEntry.Collection(m => m.Aulas)
                    .Query()
                    .Include(a => a.AulasAlunos)
                    .OrderBy(a => a.NumeroAula)
                    .Load();
            }

            // Filtrar AulasAlunos pelo AlunoId

            var aulasAlunosToRemove = new List<AulasAlunos>();

            foreach (var modulo in curso.Modulos)
            {
                foreach (var aula in modulo.Aulas)
                {
                    foreach (var aulaAluno in aula.AulasAlunos)
                    {
                        if (aulaAluno.AlunoId != alunoId)
                        {
                            aulasAlunosToRemove.Add(aulaAluno);
                        }
                    }

                    foreach (var item in aulasAlunosToRemove)
                    {
                        aula.AulasAlunos.Remove(item);
                    }
                }
            }

            ViewData["AlunoId"] = alunoId;

            return View(curso);
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
                _dbContext.AulasAlunos.Add(new AulasAlunos { AlunoId = alunoId, AulaId = aulaId, Anotacoes = anotacoes });
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

                throw;
            }
            

            return Ok();
        }
        public string GetAnotacoes(int aulaId, int alunoId)
        {
            var anotacoes = _dbContext.AulasAlunos.
                Where(aa => aa.AlunoId == alunoId).
                Where(aa => aa.AulaId == aulaId).Select(aa => aa.Anotacoes)
                .SingleOrDefault();

            return (anotacoes != null ? anotacoes : "");
        }
        [HttpPost]
        public IActionResult UpdateSalaAulaStatus(int aulaId, int alunoId, bool assistindo, bool assistida)
        {
            var aulasAluno = _dbContext.AulasAlunos.Where(aa => aa.AlunoId == alunoId).ToList();

            foreach (var item in aulasAluno)
            {
                item.Assistindo = false;
            }

            AulasAlunos aa = _dbContext.AulasAlunos
                .Where(aa => aa.AlunoId == alunoId)
                .Where(aa => aa.AulaId == aulaId)
                .FirstOrDefault();

            if (aa == null)
            {
                _dbContext.AulasAlunos.Add(new AulasAlunos { AulaId = aulaId, AlunoId = alunoId, Assistida = assistida, Assistindo = assistindo });

            }
            else
            {
                aa.Assistida = assistida;
                aa.Assistindo = assistindo;
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
