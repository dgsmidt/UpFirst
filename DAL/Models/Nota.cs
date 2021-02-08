using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int ModuloId { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public decimal Valor { get; set; }
        public Modulo Modulo { get; set; }
        public Aluno Aluno { get; set; }
    }
}
