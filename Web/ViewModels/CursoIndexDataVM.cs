using DAL.Models;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class CursoIndexDataVM
    {
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<Aula> Aulas { get; set; }
    }
}
