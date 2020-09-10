namespace DAL.Models
{
    public class AulasAlunos
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public int AlunoId { get; set; }
        public bool Assistida { get; set; }
        public bool Assistindo { get; set; }
        public string Anotacoes { get; set; }
        public Aula Aula { get; set; }
        public Aluno Aluno { get; set; }
    }
}
