using DAL.Models;
using System.Collections.Generic;

namespace Upfirst.ViewModels
{
    public class PerguntasAvaliacaoVM
    {
        public int SelectedId { get; set; }
        public int AvaliacaoId { get; set; }
        public List<PerguntaAvaliacao> PerguntasAvaliacao { get; set; }
        public List<RespostaAvaliacao> RespostasAvaliacao { get; set; }

    }
}
