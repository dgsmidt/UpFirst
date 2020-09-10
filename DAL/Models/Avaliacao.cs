using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        [Display(Name = "Description")]
        public string Descricao { get; set; }
        public Modulo Modulo { get; set; }
        public List<PerguntaAvaliacao> Perguntas { get; set; }
        
    }
}
