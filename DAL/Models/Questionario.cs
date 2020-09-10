using System.Collections.Generic;

namespace DAL.Models
{
    public class Questionario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<PerguntaQuestionario> Perguntas { get; set; }
    }
}
