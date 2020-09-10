using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class PerguntaAvaliacao
    {
        public int Id { get; set; }
        [Display(Name ="Evaluation")]
        public int AvaliacaoId { get; set; }
        [Display(Name ="Question")]
        public string Descricao { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public List<RespostaAvaliacao> Respostas { get; set; }
    }
}
