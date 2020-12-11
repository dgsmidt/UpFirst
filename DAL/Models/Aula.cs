using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        [Display(Name = "Lesson Sequence")]
        public int NumeroAula { get; set; }  // Sequencia da aula
        [Display(Name = "Description")]
        public string Descricao { get; set; }
        [Display(Name = "Online Video")]
        public string Video { get; set; }
        public string MaterialApoio { get; set; }
        [Display(Name = "Module")]
        public Modulo Modulo { get; set; }
        public List<AulaAluno> AulasAlunos { get; set; }
        public IList<ArquivoApoio> ArquivosApoio { get; set; }
    }
}
