using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class UpFirstDbContext : DbContext
    {
        public UpFirstDbContext(DbContextOptions<UpFirstDbContext> options)
            : base(options)
        // Para usar Migrations:
        // Definir o projeto Web como projeto de inicializacao
        // Definir o projeto padrao como DAL no Console do Gerenciador de Pacotes
        // add-migration Initial -c upfirstdbcontext
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Local
                //optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=Upfirst;Trusted_Connection=True;MultipleActiveResultSets=true", x => x.UseNetTopologySuite());

                // Mochahost
                //optionsBuilder.UseSqlServer(@"Data Source=198.38.83.200;Initial Catalog=ublack_upfirst;User ID=ublack_daniel;Password=MTc9zMWNIo8QS3xBGO;", x => x.UseNetTopologySuite());
            }
        }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<PerguntaAvaliacao> PerguntasAvaliacao { get; set; }
        public DbSet<RespostaAvaliacao> RespostasAvaliacao { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<PerguntaQuestionario> PerguntasQuestionario { get; set; }
        public DbSet<Questionario> Questionarios { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<AulaAluno> AulasAlunos { get; set; }
        public DbSet<ModuloAluno> ModulosAlunos { get; set; }
        public DbSet<CursoAluno> CursosAlunos { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<MercadoPago_Ipn> MercadoPago_Ipns { get; set; }
        public DbSet<MercadoPago_WebHook> MercadoPago_WebHooks { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ArquivoApoio> ArquivosApoio { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
           

            SeedAlunos(modelBuilder);
            SeedCursos(modelBuilder);
            SeedAvaliacoes(modelBuilder);
            SeedModulos(modelBuilder);
            SeedAulas(modelBuilder);
            SeedQuestionarios(modelBuilder);
            SeedPerguntasQuestionarios(modelBuilder);
            SeedPerguntasAvaliacao(modelBuilder);
            SeedRespostasAvaliacao(modelBuilder);
            SeedConfiguracoes(modelBuilder);
            //SeedCursosAlunos(modelBuilder);
        }
        private void SeedQuestionarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questionario>().HasData(
                 new Questionario { Id = 1, Descricao = "Questionário Inicial" }
             );
        }
        private void SeedConfiguracoes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuracao>().HasData(
                 new Configuracao
                 {
                     Id = 1,
                     Texto1_Index = "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!",
                     CabecalhoTexto1_Index = "Suas finanças de maneira inteligente",
                     Logo = "/assets/images/upfirst_logo.svg"
                 }
             );
        }
        private void SeedPerguntasQuestionarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerguntaQuestionario>().HasData(
                 new PerguntaQuestionario { Id = 1, QuestionarioId = 1, Descricao = "PLANEJAMENTO FINANCEIRO" },
                 new PerguntaQuestionario { Id = 2, QuestionarioId = 1, Descricao = "CONHECE O QUE É BOLSA DE VALORES?" },
                 new PerguntaQuestionario { Id = 3, QuestionarioId = 1, Descricao = "OUTRAS PERGUNTAS PARA O USUÁRIO" },
                 new PerguntaQuestionario { Id = 4, QuestionarioId = 1, Descricao = "MAIS UMA PERGUNTA AQUI" }
             );
        }
        private void SeedCursos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Nome = "FUNDAMENTAL", Preco = 5.1M },
                new Curso { Id = 2, Nome = "EDUCAÇÃO FINANCEIRA", Preco = 5.2M },
                new Curso { Id = 3, Nome = "INVESTIMENTOS", Preco = 5.3M }
                );
        }
        private void SeedModulos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Descricao = "Modulo 1", NumeroModulo = 1, CursoId = 1, AvaliacaoId = 1 },
                new Modulo { Id = 2, Descricao = "Modulo 2", NumeroModulo = 2, CursoId = 1 },
                new Modulo { Id = 3, Descricao = "Modulo 1", NumeroModulo = 1, CursoId = 2 }
                );
        }
        //private void SeedCursosAlunos(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CursoAluno>().HasData(
        //        new CursoAluno { Id = 1, CursoId = 1, AlunoId = 1, Liberado = true },
        //        new CursoAluno { Id = 2, CursoId = 2, AlunoId = 1, Liberado = true },
        //        new CursoAluno { Id = 3, CursoId = 3, AlunoId = 1, Liberado = true },
        //        new CursoAluno { Id = 4, CursoId = 1, AlunoId = 2, Liberado = true },
        //        new CursoAluno { Id = 5, CursoId = 2, AlunoId = 2, Liberado = true },
        //        new CursoAluno { Id = 6, CursoId = 3, AlunoId = 2, Liberado = true },
        //        new CursoAluno { Id = 7, CursoId = 1, AlunoId = 3, Liberado = true },
        //        new CursoAluno { Id = 8, CursoId = 2, AlunoId = 3, Liberado = true },
        //        new CursoAluno { Id = 9, CursoId = 3, AlunoId = 3, Liberado = true },
        //        new CursoAluno { Id = 10, CursoId = 1, AlunoId = 4, Liberado = true },
        //        new CursoAluno { Id = 11, CursoId = 2, AlunoId = 4, Liberado = true },
        //        new CursoAluno { Id = 12, CursoId = 3, AlunoId = 4, Liberado = true },
        //        new CursoAluno { Id = 13, CursoId = 1, AlunoId = 5, Liberado = true },
        //        new CursoAluno { Id = 14, CursoId = 2, AlunoId = 5, Liberado = true },
        //        new CursoAluno { Id = 15, CursoId = 3, AlunoId = 5, Liberado = true },
        //        new CursoAluno { Id = 16, CursoId = 1, AlunoId = 6, Liberado = true },
        //        new CursoAluno { Id = 17, CursoId = 2, AlunoId = 6, Liberado = true },
        //        new CursoAluno { Id = 18, CursoId = 3, AlunoId = 6, Liberado = true },
        //        new CursoAluno { Id = 19, CursoId = 1, AlunoId = 7, Liberado = true },
        //        new CursoAluno { Id = 20, CursoId = 2, AlunoId = 7, Liberado = true },
        //        new CursoAluno { Id = 21, CursoId = 3, AlunoId = 7, Liberado = true }
        //        );
        //}
        private void SeedAulas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aula>().HasData(
                new Aula { Id = 1, Descricao = "Aula 1", ModuloId = 1, Video = "https://www.youtube.com/watch?v=eehO6YQycBQ", NumeroAula = 1 },
                new Aula { Id = 2, Descricao = "Aula 2", ModuloId = 1, Video = "https://www.youtube.com/watch?v=5niylfZuZ8k", NumeroAula = 2 },
                new Aula { Id = 3, Descricao = "Aula 3", ModuloId = 1, Video = "https://www.youtube.com/watch?v=wHsG4G3evWE", NumeroAula = 3 },
                new Aula { Id = 4, Descricao = "Aula 4", ModuloId = 1, Video = "https://www.youtube.com/watch?v=_DYno3fsLEw", NumeroAula = 4 },
                new Aula { Id = 5, Descricao = "Aula 1", ModuloId = 2, Video = "https://www.youtube.com/watch?v=fnv-o1kFI6g", NumeroAula = 1 },
                new Aula { Id = 6, Descricao = "Aula 1", ModuloId = 3, Video = "https://www.youtube.com/watch?v=PPbPy7BNvBs", NumeroAula = 1 }
                );
        }
        private void SeedAvaliacoes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avaliacao>().HasData(
                new Avaliacao { Id = 1, Descricao = "Avaliação 1", ModuloId = 1 }
                );
        }

        private void SeedAlunos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno { Id = 1, UserId = "Teste", NotaQuestionario = 0, Nome = "Teste", Email = "teste@teste.com", WhatsApp = "12999888877" },
                new Aluno { Id = 2, UserId = "194ee88d-44fd-4168-b360-8da5c600726c", NotaQuestionario = 4.5M, Nome = "Cláudio", Email = "claudio.rosa@gswsoftware.com" },
                new Aluno { Id = 4, UserId = "7cb31e03-5a94-4527-b44d-a6791d20d842", NotaQuestionario = 0, Nome = "Cláudio", Email = "csrclaudio@gmail.com" },
                new Aluno { Id = 5, UserId = "966a4985-0049-405a-9685-38c37a03ca39", NotaQuestionario = 2.8M, Nome = "Marciley", Email = "marcileychristovao@uol.com.br" },
                new Aluno { Id = 6, UserId = "9adc3d2f-34f7-4c22-9ef2-2c19d8c8b7c4", NotaQuestionario = 0, Nome = "Cláudio", Email = "claudio_vilanova@yahoo.com.br" },
                new Aluno { Id = 7, UserId = "cf1b9d7f-9881-4437-bcbb-0e32a6ec2525", NotaQuestionario = 2.0M, Nome = "Daniel", Email = "daniel.smidt@yahoo.com.br" }
                );
        }
        private void SeedPerguntasAvaliacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerguntaAvaliacao>().HasData(
                new PerguntaAvaliacao { Id = 1, Descricao = "Quanto é 1 x 1 ?", AvaliacaoId = 1 },
                new PerguntaAvaliacao { Id = 2, Descricao = "Quanto é 2 x 6 ?", AvaliacaoId = 1 }
                );
        }
        private void SeedRespostasAvaliacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RespostaAvaliacao>().HasData(
                new RespostaAvaliacao { Id = 1, Descricao = "Zero", Correta = false, PerguntaAvaliacaoId = 1 },
                new RespostaAvaliacao { Id = 2, Descricao = "Um", Correta = true, PerguntaAvaliacaoId = 1 },
                new RespostaAvaliacao { Id = 3, Descricao = "Dois", Correta = false, PerguntaAvaliacaoId = 1 },
                new RespostaAvaliacao { Id = 4, Descricao = "Três", Correta = false, PerguntaAvaliacaoId = 1 },
                new RespostaAvaliacao { Id = 5, Descricao = "6", Correta = false, PerguntaAvaliacaoId = 2 },
                new RespostaAvaliacao { Id = 6, Descricao = "13", Correta = false, PerguntaAvaliacaoId = 2 },
                new RespostaAvaliacao { Id = 7, Descricao = "12", Correta = true, PerguntaAvaliacaoId = 2 },
                new RespostaAvaliacao { Id = 8, Descricao = "14", Correta = false, PerguntaAvaliacaoId = 2 }
                );
        }
        public async Task ExcluirAlunoAsync(string userId)
        {
            var aluno = await Alunos.Where(a => a.UserId == userId).SingleOrDefaultAsync();

            if (aluno != null)
            {
                Remove(aluno);

                await SaveChangesAsync();
            }
        }
    }
}
