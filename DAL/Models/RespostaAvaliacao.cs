using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DAL.Models
{
    public class RespostaAvaliacao
    {
        public int Id { get; set; }
        public int PerguntaAvaliacaoId { get; set; }
        [Display(Name = "Answer")]
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public PerguntaAvaliacao Pergunta { get; set; }
    }
}
