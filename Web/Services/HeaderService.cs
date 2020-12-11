using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class HeaderService : ICursosAlunoService
    {
        private readonly UpFirstDbContext _dbContext;
        public HeaderService(UpFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<string> GetLogoPathAsync()
        {
            string logoPath = _dbContext.Configuracoes.Select(c=>c.Logo).SingleOrDefault();

            return Task.FromResult(logoPath);
        }

        public Task<List<Curso>> GetCursosAlunoAsync(string email)
        {
            List<Curso> cursos = new List<Curso>();

            Aluno aluno = _dbContext.Alunos.Where(a => a.Email == email).SingleOrDefault();

            if (aluno != null)
            {
                IEnumerable<CursoAluno> cursosAluno = _dbContext.CursosAlunos
                    .Include(ca => ca.Curso)
                    .Where(ca => ca.AlunoId == aluno.Id && ca.Liberado)
                    .ToList();

                foreach (var item in cursosAluno)
                {
                    cursos.Add(new Curso { Id = item.CursoId, Nome = item.Curso.Nome });
                }
            }

            return Task.FromResult(cursos);
            //throw new NotImplementedException();
        }
    }
}
