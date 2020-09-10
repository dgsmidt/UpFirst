using System.Collections.Generic;

namespace DAL.Models
{
    public class Evolucao
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public List<Etapa> Etapas { get; set; }
    }
}
