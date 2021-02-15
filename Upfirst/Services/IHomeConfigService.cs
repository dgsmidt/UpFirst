using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Upfirst.Services
{
    public interface IHomeConfigService
    {
        Task<List<Curso>> GetMatriculasLiberadasAsync(string email);
        Task<string> GetLogoPathAsync();
        string GetTitle();
        string GetLogoBackground();
        Task<int> GetAlunoIdAsync(string UserName);
        Task<string> GetLinhaEnderecoAsync(int linha);
    }
}
