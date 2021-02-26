using DAL.Models;
using System.Collections.Generic;

namespace Upfirst.ViewModels
{
    public class CursosVM
    {
        public int SelectedId { get; set; }
        public int ModuloId { get; set; }
        public List<Curso> Cursos { get; set; }
        public List<Modulo> Modulos { get; set; }
        public List<Aula> Aulas { get; set; }
    }
}
