using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        [ForeignKey("Avaliacao")]
        public int? AvaliacaoId { get; set; }
        [Display(Name = "Module Name")]
        public string Descricao { get; set; }
        [Display(Name = "Module Sequence")]
        public int NumeroModulo { get; set; }  // Sequencia do modulo
        [NotMapped]
        public bool Liberado { get; set; }
        [NotMapped]
        public bool AvaliacaoLiberada { get; set; }
        public Curso Curso { get; set; }
        public List<Aula> Aulas { get; set; }
        public Avaliacao Avaliacao { get; set; }
    }
}
