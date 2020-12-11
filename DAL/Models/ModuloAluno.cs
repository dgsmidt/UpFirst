using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class ModuloAluno
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int ModuloId { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public decimal? Nota { get; set; }
        public bool Liberado { get; set; }
        public bool AvaliacaoLiberada { get; set; }
        public int  NumeroModulo { get; set; }
        public Modulo Modulo { get; set; }
        public Aluno Aluno { get; set; }
        public List<AulaAluno> AulasAlunos { get; set; }
    }
}
