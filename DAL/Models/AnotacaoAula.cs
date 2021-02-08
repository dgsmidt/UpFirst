using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class AnotacaoAula
    {
        public int Id { get; set; }
        public int AlunoId{ get; set; }
        public int AulaId { get; set; }
        public string Anotacao { get; set; }
        public Aluno Aluno { get; set; }
        public Aula Aula { get; set; }
    }
}
