using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace DAL.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name= "Questionnaire")]
        public decimal NotaQuestionario { get; set; }
        public List<Nota> Notas { get; set; }
        public List<AulasAlunos> AulasAlunos { get; set; }
        public List<CursosAlunos> CursosAlunos { get; set; }
    }
}
