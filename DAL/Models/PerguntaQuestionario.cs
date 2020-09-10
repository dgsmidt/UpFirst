using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class PerguntaQuestionario
    {
        public int Id { get; set; }
        public int QuestionarioId { get; set; }

        [Display(Name = "Description")]
        public string Descricao { get; set; }
        public string Resposta { get; set; }
        public Questionario Questionario { get; set; }
    }
}
