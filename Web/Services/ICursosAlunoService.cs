using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface ICursosAlunoService
    {
        Task<List<Curso>> GetCursosAlunoAsync(string email);
    }
}
