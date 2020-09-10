using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Curso
    {
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        public string Nome { get; set; }
        [Display(Name = "Description")]
        public string Descricao { get; set; }
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Preco { get; set; }
        public List<Modulo> Modulos { get; set; }
        public List<CursosAlunos> CursosAlunos { get; set; }

    }
}
