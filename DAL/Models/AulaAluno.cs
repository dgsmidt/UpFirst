namespace DAL.Models
{
    public class AulaAluno
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public int AlunoId { get; set; }
        public bool Assistida { get; set; }
        public bool Assistindo { get; set; }
        public string Anotacoes { get; set; }
        public bool HabilitarAssistida { get; set; }
        public int NumeroAula { get; set; }
        public Aula Aula { get; set; }
        public Aluno Aluno { get; set; }
    }
}
