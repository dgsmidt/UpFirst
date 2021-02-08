using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class HomeConfigService : IHomeConfigService
    {
        private readonly UpFirstDbContext _dbContext;
        public HomeConfigService(UpFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<string> GetLogoPathAsync()
        {
            string logoPath = _dbContext.Configuracoes.Select(c => c.Logo).FirstOrDefault();

            return Task.FromResult(logoPath);
        }
        public string GetTitle()
        {
            string title = _dbContext.Configuracoes.Select(c => c.Titulo).FirstOrDefault();

            //return Task.FromResult(title);
            return title;
        }
        public string GetLogoBackground()
        {
            string logoBg = _dbContext.Configuracoes.Select(c => c.LogoBackground).FirstOrDefault();

            //return Task.FromResult(title);
            return logoBg;
        }
        public async Task<int> GetAlunoIdAsync(string UserName)
        {
            if (UserName != null)
            {
                int alunoId = await _dbContext.Alunos.Where(a => a.Email == UserName).Select(a => a.Id).SingleOrDefaultAsync();
                return alunoId;
            }
            else
            {
                return 0;
            }
        }
        public async Task<string> GetLinhaEnderecoAsync(int linha)
        {
            string strLinha = string.Empty;

            switch (linha)
            {
                case 1:
                    strLinha = await _dbContext.Configuracoes.Select(c => c.EnderecoLinha1).FirstOrDefaultAsync();
                    break;
                case 2:
                    strLinha = await _dbContext.Configuracoes.Select(c => c.EnderecoLinha2).FirstOrDefaultAsync();
                    break;
                case 3:
                    strLinha = await _dbContext.Configuracoes.Select(c => c.EnderecoLinha3).FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }

            return strLinha;
        }
        public async Task<List<Curso>> GetMatriculasLiberadasAsync(string email)
        {
            List<Curso> cursos = new List<Curso>();

            Aluno aluno = await _dbContext.Alunos.Where(a => a.Email == email).SingleOrDefaultAsync();

            if (aluno != null)
            {
                IEnumerable<Matricula> matriculas = await _dbContext.Matriculas
                    .Include(m => m.Curso)
                    .Where(m => m.AlunoId == aluno.Id && m.Liberada)
                    .ToListAsync();

                foreach (var item in matriculas)
                {
                    cursos.Add(new Curso { Id = item.CursoId, Nome = item.Curso.Nome });
                }
            }

            return cursos;
            //throw new NotImplementedException();
        }

    }
}
