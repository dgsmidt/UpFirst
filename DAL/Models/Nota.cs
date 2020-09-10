using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int ModuloId { get; set; }
        public decimal Valor { get; set; }
        public Aluno Aluno { get; set; }
        public Modulo Modulo { get; set; }
    }
}
