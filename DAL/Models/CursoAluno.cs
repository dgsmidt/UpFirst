using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CursoAluno
    {
        public int Id { get; set; }
        [Display(Name ="Date")]
        public DateTime Data { get; set; }
        public int CursoId { get; set; }
        public int AlunoId { get; set; }
        [Display(Name = "Released")]
        public bool Liberado { get; set; }
        [Display (Name = "Course")]
        public Curso Curso { get; set; }
        [Display(Name = "Student")]
        public Aluno Aluno { get; set; }
        public List<ModuloAluno> ModulosAlunos { get; set; }

    }
}
