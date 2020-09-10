using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        [Display(Name = "Module Name")]
        public string Descricao { get; set; }
        [Display(Name = "Module Sequence")]
        public int NumeroModulo { get; set; }  // Sequencia do modulo
        public Curso Curso { get; set; }
        public List<Aula> Aulas { get; set; }
    }
}
