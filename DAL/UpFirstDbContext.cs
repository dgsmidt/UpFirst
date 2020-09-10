using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<AulasAlunos> AulasAlunos { get; set; }
        public DbSet<CursosAlunos> CursosAlunos { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<MercadoPago_Ipn> MercadoPago_Ipns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            modelBuilder.Entity<Nota>()
                .Property(n => n.Valor).HasColumnType("decimal(3,1)");

            modelBuilder.Entity<Aluno>()
                .Property(n => n.NotaQuestionario).HasColumnType("decimal(3,1)");

            SeedAlunos(modelBuilder);
            SeedCursos(modelBuilder);
            SeedModulos(modelBuilder);
            SeedAulas(modelBuilder);
            SeedQuestionarios(modelBuilder);
            SeedPerguntasQuestionarios(modelBuilder);
            SeedAvaliacoes(modelBuilder);
            SeedPerguntasAvaliacao(modelBuilder);
            SeedRespostasAvaliacao(modelBuilder);
            SeedConfiguracoes(modelBuilder);
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
                 new Configuracao { 
                     Id = 1, 
                     Texto1_Index = "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!",
                     CabecalhoTexto1_Index= "Suas finanças de maneira inteligente" }
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
                new Curso { Id = 1, Nome = "FUNDAMENTAL", Preco = 162.67M },
                new Curso { Id = 2, Nome = "EDUCAÇÃO FINANCEIRA", Preco = 84.67M },
                new Curso { Id = 3, Nome = "INVESTIMENTOS", Preco = 172.67M }


                );
        }
        private void SeedModulos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 1, Descricao = "Modulo 1", NumeroModulo = 1, CursoId = 1 },
                new Modulo { Id = 2, Descricao = "Modulo 2", NumeroModulo = 2, CursoId = 1 },
                new Modulo { Id = 3, Descricao = "Modulo 1", NumeroModulo = 1, CursoId = 2 }
                );
        }
        private void SeedAulas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aula>().HasData(
                new Aula { Id = 1, Descricao = "Aula 1", ModuloId = 1, Video = "https://player.vimeo.com/video/141439971", NumeroAula = 1 },
                new Aula { Id = 2, Descricao = "Aula 2", ModuloId = 1, Video = "https://player.vimeo.com/video/141561250", NumeroAula = 2 },
                new Aula { Id = 3, Descricao = "Aula 3", ModuloId = 1, Video = "https://player.vimeo.com/video/444387842", NumeroAula = 3 },
                new Aula { Id = 4, Descricao = "Aula 4", ModuloId = 1, Video = "https://player.vimeo.com/video/116629498", NumeroAula = 4 },
                new Aula { Id = 5, Descricao = "Aula 1", ModuloId = 2, Video = "https://player.vimeo.com/video/436144408", NumeroAula = 1 },
                new Aula { Id = 6, Descricao = "Aula 1", ModuloId = 3, Video = "https://player.vimeo.com/video/116619880", NumeroAula = 1 }
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
                new Aluno { Id = 1, UserId = "Teste" }
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
