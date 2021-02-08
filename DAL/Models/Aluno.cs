using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace DAL.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Name")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        [Display(Name = "Questionnaire")]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal NotaQuestionario { get; set; }
    }
}
