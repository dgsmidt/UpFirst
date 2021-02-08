using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public int PagamentoId { get; set; }
        public bool Liberada { get; set; }
        public bool CursoConcluido { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public Pagamento Pagamento { get; set; }


    }
}
