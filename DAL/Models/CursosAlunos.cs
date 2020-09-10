using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CursosAlunos
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int AlunoId { get; set; }
        public bool Liberado { get; set; }
        public Curso Curso { get; set; }
        public Aluno Aluno { get; set; }
    }
}
