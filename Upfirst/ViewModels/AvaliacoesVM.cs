using DAL.Models;
using System.Collections.Generic;

namespace Upfirst.ViewModels
{
    public class AvaliacoesVM
    {
        public int SelectedId { get; set; }
        public int PerguntaId { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public List<PerguntaAvaliacao> PerguntasAvaliacao { get; set; }
        public List<RespostaAvaliacao> RespostasAvaliacao { get; set; }
    }
}
