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
        // update-database -c upfirstdbcontext
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
        //public DbSet<AulaAluno> AulasAlunos { get; set; }
        //public DbSet<ModuloAluno> ModulosAlunos { get; set; }
        //public DbSet<CursoAluno> CursosAlunos { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        //public DbSet<MercadoPago_Ipn> MercadoPago_Ipns { get; set; }
        public DbSet<MercadoPago_WebHook> MercadoPago_WebHooks { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ArquivoApoio> ArquivosApoio { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<AnotacaoAula> AnotacoesAulas { get; set; }
        public DbSet<StatusAulas> StatusAulas { get; set; }
        public DbSet<AulaAssistida> AulasAssistidas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Desconto> Descontos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //SeedAlunos(modelBuilder);
            SeedCursos(modelBuilder);
            SeedAvaliacoes(modelBuilder);
            SeedModulos(modelBuilder);
            SeedAulas(modelBuilder);
            SeedArquivosApoio(modelBuilder);
            SeedQuestionarios(modelBuilder);
            SeedPerguntasQuestionarios(modelBuilder);
            SeedPerguntasAvaliacao(modelBuilder);
            SeedRespostasAvaliacao(modelBuilder);
            SeedConfiguracoes(modelBuilder);
            //SeedCursosAlunos(modelBuilder);
        }
        public async Task RemoverAlunoAsync(string userId)
        {
            Aluno aluno = await Alunos.Where(a => a.UserId == userId).SingleOrDefaultAsync();

            if (aluno != null)
            {
                await RemoverDadosAlunoAsync(aluno.Id);
                Alunos.Remove(aluno);
                await SaveChangesAsync();
            }
        }
        public async Task RemoverDadosAlunoAsync(int alunoId)
        {
            var aluno = await Alunos.FindAsync(alunoId);

            // Apagar aulas assistidas
            var aulasAssistidas = await AulasAssistidas.Where(aa => aa.AlunoId == aluno.Id).ToListAsync();

            if (aulasAssistidas != null)
            {
                AulasAssistidas.RemoveRange(aulasAssistidas);
            }

            // Apagar anotacoes
            var anotacoes = await AnotacoesAulas.Where(aa => aa.AlunoId == aluno.Id).ToListAsync();

            if (anotacoes != null)
            {
                AnotacoesAulas.RemoveRange(anotacoes);
            }

            // Apagar notas
            var notas = await Notas.Where(n => n.AlunoId == aluno.Id).ToListAsync();

            if (notas != null)
            {
                Notas.RemoveRange(notas);
            }

            // Apagar matriculas
            var matriculas = await Matriculas.Where(m => m.AlunoId == aluno.Id).ToListAsync();

            if (matriculas != null)
            {
                // Apagar Status Aulas
                foreach (var matricula in matriculas)
                {
                    var statusAulas = StatusAulas.Where(sa => sa.MatriculaId == matricula.Id).SingleOrDefault();
                    if (statusAulas != null)
                    {
                        StatusAulas.Remove(statusAulas);
                    }

                    var pagamento = await Pagamentos.Where(p => p.Id == matricula.PagamentoId).SingleOrDefaultAsync();

                    Pagamentos.Remove(pagamento);
                    Matriculas.Remove(matricula);
                }
            }

            await SaveChangesAsync();
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
                     Titulo = "ESF",
                     EmailContato = "contato@upfirst.com.br",
                     Texto1_Index = "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!",
                     CabecalhoTexto1_Index = "Suas finanças de maneira inteligente",
                     Logo = "/assets/logos/Logo_escuro_2a11.jpg",
                     LogoBackground = "RGB(6,26,55)",
                     Video_Index = "/uploads/Institucional_Oficial_v.1.mp4",
                     CabecalhoTexto2_Index = "CONHEÇA NOSSOS CURSOS",
                     Texto2_Index = "Sucesso financeiro é ter dinheiro suficiente para fazer aquilo que você deseja, de forma planejada. Nossos cursos te dará a direção, e proporcionarão ferramentas para que você se torne uma pessoa financeiramente bem-sucedida, ter dinheiro suficiente para cobrir o seu custo de vida e realizar projetos futuros, sem precisar se endividar, por exemplo.",
                     CabecalhoTexto3_Index = "COMO FUNCIONA",
                     Texto3_Index = "Você aprenderá a prosperar financeiramente através de 5 bases simples: 1. Mudança de mentalidade financeira; 2. Poupar; 3. Investir; 4. Renda Extra; e 5. Simplificação. Você descobrirá oportunidades incríveis para aumentar as receitas, controlar as despesas, investir em ativos de alta rentabilidade, viver um estilo de vida simples e abundante! Quando você enriquece o mundo se torna um lugar melhor!",
                     TextoAlvo_Index = "A meta é a vida abundante! O que você não tem é pelo que você ainda não conhece. Você possui uma mente infinitamente criativa, uma capacidade infinita de ser uma pessoa rica. Só precisa aprender a usá-la.",
                     TextoGrafico_Index = "A parte mais trabalhosa de enriquecer é tomar deliberadamente a decisão de começar. Todas as demais etapas são bem mais simples. O que você precisa fazer agora é decidir se tornar uma pessoa próspera financeiramente e começar a agir!",
                     TextoComputador_Index = "Pesquisas demonstram que 95% das pessoas se aponsentam com renda insuficiente na velhice, e dependem do governo ou de parentes. Milhões de brasileiros vivem essa realidade. Você pode planejar agora as finanças dos próximos anos e virar essa equação em seu favor. Te pegaremos pela mão e te auxiliaremos a planejar a sua reserva financeira para hoje e para o futuro.",
                     EnderecoLinha1 = "Av. Dr. Nelson d''Ávila, 1837",
                     EnderecoLinha2 = "Centro - Sao Jose dos Campos",
                     EnderecoLinha3 = "",
                     NotaDeCorte = 5
                 }
             );
        }
        private void SeedPerguntasQuestionarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerguntaQuestionario>().HasData(
                 new PerguntaQuestionario { Id = 7, QuestionarioId = 1, Descricao = "Qual seu conhecimento em despesas e receitas?" },
                 new PerguntaQuestionario { Id = 8, QuestionarioId = 1, Descricao = "Qual seu conhecimento em investimentos?" },
                 new PerguntaQuestionario { Id = 9, QuestionarioId = 1, Descricao = "Qual o seu conhecimento em controle financeiro?" }
             );
        }
        private void SeedCursos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 2, Nome = "EDUCAÇÃO FINANCEIRA", Preco = 399.00M }
                );
        }
        private void SeedArquivosApoio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArquivoApoio>().HasData(
                new ArquivoApoio { Id = 1, AulaId = 11, FileName = "/uploads/1 E se ... Modulo_1_aula_2.pdf" },
                new ArquivoApoio { Id = 2, AulaId = 11, FileName = "/uploads/2 Questionário Financeiro_Modulo_1_aula_2.pdf" },
                new ArquivoApoio { Id = 3, AulaId = 12, FileName = "/uploads/1 Anamnese Financeira_Modulo_1_aula_3.pdf" },
                new ArquivoApoio { Id = 4, AulaId = 13, FileName = "/uploads/Parâmetros do MAF_Modulo_1_aula_4.pdf" },
                new ArquivoApoio { Id = 5, AulaId = 7, FileName = "/uploads/Caderno de exercício__Modulo_2_aula_1.pdf" },
                new ArquivoApoio { Id = 6, AulaId = 15, FileName = "/uploads/Caderno de exercícios_Modulo_2_aula_2.pdf" },
                new ArquivoApoio { Id = 9, AulaId = 8, FileName = "/uploads/Pirâmide do Indivíduo_Modulo_3_aula_1.pdf" },
                new ArquivoApoio { Id = 13, AulaId = 14, FileName = "/uploads/Caderno de Exercícios__Modulo_1_aula_5.pdf" },
                new ArquivoApoio { Id = 14, AulaId = 16, FileName = "/uploads/Caderno de exercício_Modulo_2_aula_3.pdf" },
                new ArquivoApoio { Id = 15, AulaId = 17, FileName = "/uploads/Caderno de exercício_Modulo_2_aula_4.pdf" },
                new ArquivoApoio { Id = 16, AulaId = 18, FileName = "/uploads/Caderno de exercício_Modulo_3_aula_2.pdf" },
                new ArquivoApoio { Id = 17, AulaId = 18, FileName = "/uploads/Planilha Negociação Dívidas_Modulo_3_aula_2.pdf" },
                new ArquivoApoio { Id = 18, AulaId = 9, FileName = "/uploads/Agenda pag 1_Modulo_4_aula_1.pdf" },
                new ArquivoApoio { Id = 19, AulaId = 9, FileName = "/uploads/Agenda pag 2_Modulo_4_aula_1.pdf" },
                new ArquivoApoio { Id = 20, AulaId = 9, FileName = "/uploads/Caderno de exercícios_Modulo_4_aula_1.pdf" }
                );
        }
        private void SeedModulos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modulo>().HasData(
                new Modulo { Id = 3, Descricao = "Modulo 1", NumeroModulo = 1, CursoId = 2, AvaliacaoId = 2 },
                new Modulo { Id = 4, Descricao = "Modulo 2", NumeroModulo = 2, CursoId = 2, AvaliacaoId = 3 },
                new Modulo { Id = 5, Descricao = "Modulo 3", NumeroModulo = 3, CursoId = 2, AvaliacaoId = 4 },
                new Modulo { Id = 6, Descricao = "Modulo 4", NumeroModulo = 4, CursoId = 2, AvaliacaoId = 5 },
                new Modulo { Id = 7, Descricao = "Modulo 5", NumeroModulo = 5, CursoId = 2, AvaliacaoId = 6 }
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
                new Aula { Id = 6, Descricao = "Aula 1", ModuloId = 3, Video = "/uploads/Aula_1.mp4", NumeroAula = 1 },
                new Aula { Id = 7, Descricao = "Aula 1", ModuloId = 4, Video = "/uploads/Mod 02 - Aula 1.mp4", NumeroAula = 1 },
                new Aula { Id = 8, Descricao = "Aula 1", ModuloId = 5, Video = "/uploads/Mod 03 - Aula 1.mp4", NumeroAula = 1 },
                new Aula { Id = 9, Descricao = "Aula 1", ModuloId = 6, Video = "/uploads/Mod 04 - Aula 1.mp4", NumeroAula = 1 },
                new Aula { Id = 10, Descricao = "Aula 1", ModuloId =7, Video = "/uploads/Mod 05 - Aula 1.mp4", NumeroAula = 1 },
                new Aula { Id = 11, Descricao = "Aula 2", ModuloId = 3, Video = "/uploads/Aula 2.mp4", NumeroAula = 2 },
                new Aula { Id = 12, Descricao = "Aula 3", ModuloId = 3, Video = "/uploads/Aula 3.mp4", NumeroAula = 3 },
                new Aula { Id = 13, Descricao = "Aula 4", ModuloId = 3, Video = "/uploads/Aula 4.mp4", NumeroAula = 4 },
                new Aula { Id = 14, Descricao = "Aula 5", ModuloId = 3, Video = "/uploads/Aula 5.mp4", NumeroAula = 5 },
                new Aula { Id = 15, Descricao = "Aula 2", ModuloId = 4, Video = "/uploads/Mod 02 - Aula 2.mp4", NumeroAula = 2 },
                new Aula { Id = 16, Descricao = "Aula 3", ModuloId = 4, Video = "/uploads/Mod 02 - Aula 3.mp4", NumeroAula = 3 },
                new Aula { Id = 17, Descricao = "Aula 4", ModuloId = 4, Video = "/uploads/Mod 02 - Aula 4.mp4", NumeroAula = 4 },
                new Aula { Id = 18, Descricao = "Aula 2", ModuloId = 5, Video = "/uploads/Mod 03 - Aula 2.mp4", NumeroAula = 2 }
                );
        }
        private void SeedAvaliacoes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avaliacao>().HasData(
                new Avaliacao { Id = 1, Descricao = "Avaliação 1", ModuloId = 1 },
                new Avaliacao { Id = 2, Descricao = "Modulo 1 - Prova", ModuloId = 3 },
                new Avaliacao { Id = 3, Descricao = "Modulo 2 - Prova", ModuloId = 4 },
                new Avaliacao { Id = 4, Descricao = "Modulo 3 - Prova", ModuloId = 5 },
                new Avaliacao { Id = 5, Descricao = "Modulo 4 - Prova", ModuloId = 6 },
                new Avaliacao { Id = 6, Descricao = "Modulo 5 - Prova", ModuloId = 7 }
                );
        }

        //private void SeedAlunos(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>().HasData(
        //        new Aluno { Id = 1, UserId = "Teste", NotaQuestionario = 0, Nome = "Teste", Email = "teste@teste.com", WhatsApp = "12999888877" },
        //        new Aluno { Id = 2, UserId = "194ee88d-44fd-4168-b360-8da5c600726c", NotaQuestionario = 4.5M, Nome = "Cláudio", Email = "claudio.rosa@gswsoftware.com" },
        //        new Aluno { Id = 4, UserId = "7cb31e03-5a94-4527-b44d-a6791d20d842", NotaQuestionario = 0, Nome = "Cláudio", Email = "csrclaudio@gmail.com" },
        //        new Aluno { Id = 5, UserId = "966a4985-0049-405a-9685-38c37a03ca39", NotaQuestionario = 2.8M, Nome = "Marciley", Email = "marcileychristovao@uol.com.br" },
        //        new Aluno { Id = 6, UserId = "9adc3d2f-34f7-4c22-9ef2-2c19d8c8b7c4", NotaQuestionario = 0, Nome = "Cláudio", Email = "claudio_vilanova@yahoo.com.br" },
        //        new Aluno { Id = 7, UserId = "cf1b9d7f-9881-4437-bcbb-0e32a6ec2525", NotaQuestionario = 2.0M, Nome = "Daniel", Email = "daniel.smidt@yahoo.com.br" }
        //        );
        //}
        private void SeedPerguntasAvaliacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerguntaAvaliacao>().HasData(
                new PerguntaAvaliacao { Id = 1, Descricao = "Quanto é 1 x 1 ?", AvaliacaoId = 1 },
                new PerguntaAvaliacao { Id = 2, Descricao = "Quanto é 2 x 6 ?", AvaliacaoId = 1 },
                new PerguntaAvaliacao { Id = 3, Descricao = "Qual alternativa não faz parte das 5 bases deste curso?", AvaliacaoId = 2 },
                new PerguntaAvaliacao { Id = 4, Descricao = "Marque a única alternativa incorreta. As 6 leis da autorresponsabilidade financeira são?", AvaliacaoId = 3 },
                new PerguntaAvaliacao { Id = 5, Descricao = "Marque as 3 alternativas corretas. Riqueza verdadeira é aquela que combina as três dimensões humanas, quais são elas?", AvaliacaoId = 4 },
                new PerguntaAvaliacao { Id = 6, Descricao = "Na aula sobre construção de RENDA EXTRA, ensinamos que para se tornar um especialista em sua área de atuação é preciso concluir um macrociclo de 1.000 horas. O macrociclo é formado por 22 semanas de microciclos aplicados. A pergunta é: um microciclo de 45 horas semanais é composto por?", AvaliacaoId = 5 },
                new PerguntaAvaliacao { Id = 7, Descricao = "Marque a única alternativa correta. Na aula sobre SIMPLIFICAÇÃO, ensinamos o seguinte conceito:", AvaliacaoId = 6 },
                new PerguntaAvaliacao { Id = 8, Descricao = "Qual alternativa não faz parte dos 11 pilares do MAF – Mapa de Autoavaliação Financeira?", AvaliacaoId = 2 },
                new PerguntaAvaliacao { Id = 10, Descricao = "De acordo com a aula 3 deste módulo 2, o segredo dos vencedores é?", AvaliacaoId = 3 },
                new PerguntaAvaliacao { Id = 11, Descricao = "De quem é a célebre frase mencionada na aula 1 deste módulo 3: “Para que as coisas mudem, você tem que mudar.... Para que as coisas melhorem, você tem que melhorar.... Podemos ter mais do que já temos, porque podemos nos tornar melhores do que somos.”.", AvaliacaoId = 4 },
                new PerguntaAvaliacao { Id = 12, Descricao = "A média de ganho de um especialista é?", AvaliacaoId = 5 },
                new PerguntaAvaliacao { Id = 13, Descricao = "A quantidade de salários sugeridos que você ganha, para compor a sua reserva financeira é?", AvaliacaoId = 6 }
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
                new RespostaAvaliacao { Id = 8, Descricao = "14", Correta = false, PerguntaAvaliacaoId = 2 },
                new RespostaAvaliacao { Id = 9, Descricao = "Mudança de mentalidade financeira", Correta = false, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 10, Descricao = "Se for criticar as pessoas, cale-se", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 11, Descricao = "Dimensão do SER, caracterizado pela CRENÇA DE IDENTIDADE", Correta = true, PerguntaAvaliacaoId = 5 },
                new RespostaAvaliacao { Id = 12, Descricao = "7 horas de trabalho, e 2 horas de estudos diários", Correta = false, PerguntaAvaliacaoId = 6 },
                new RespostaAvaliacao { Id = 13, Descricao = "Aumente o seu padrão de vida à medida em que sua renda aumentar", Correta = false, PerguntaAvaliacaoId = 7 },
                new RespostaAvaliacao { Id = 14, Descricao = "Poupar", Correta = false, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 15, Descricao = "Investir", Correta = false, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 16, Descricao = "Renda extra", Correta = false, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 17, Descricao = "Simplificação", Correta = false, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 18, Descricao = "Gastar", Correta = true, PerguntaAvaliacaoId = 3 },
                new RespostaAvaliacao { Id = 19, Descricao = "Se for reclamar das circunstâncias, dê sugestão", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 20, Descricao = "Se for investir, invista na caderneta de poupança", Correta = true, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 21, Descricao = "Se for buscar culpados, busque a solução", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 22, Descricao = "Se for se fazer de vítima, faça-se de vencedor", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 23, Descricao = "Se for justificar os seus erros, aprenda com eles", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 24, Descricao = "Se for julgar alguém, julgue a atitude dessa pessoa", Correta = false, PerguntaAvaliacaoId = 4 },
                new RespostaAvaliacao { Id = 25, Descricao = "Dimensão do FAZER, caracterizado pela CRENÇA DE CAPACIDADE", Correta = true, PerguntaAvaliacaoId = 5 },
                new RespostaAvaliacao { Id = 26, Descricao = "Dimensão do TER, caracterizado pela CRENÇA DE MERECIMENTO", Correta = true, PerguntaAvaliacaoId = 5 },
                new RespostaAvaliacao { Id = 27, Descricao = "Dimensão da ECONOMIA, caracterizado pela CRENÇA DE POUPAR", Correta = false, PerguntaAvaliacaoId = 5 },
                new RespostaAvaliacao { Id = 28, Descricao = "Dimensão do TRABALHO, caracterizado pela CRENÇA DE PRODUZIR", Correta = false, PerguntaAvaliacaoId = 5 },
                new RespostaAvaliacao { Id = 29, Descricao = "6 horas de trabalho, e 3 horas de estudos diários", Correta = false, PerguntaAvaliacaoId = 6 },
                new RespostaAvaliacao { Id = 30, Descricao = "9 horas de trabalho, e 1 hora de estudos diários", Correta = false, PerguntaAvaliacaoId = 6 },
                new RespostaAvaliacao { Id = 31, Descricao = "8 horas de trabalho, e 1 hora de estudos diários", Correta = true, PerguntaAvaliacaoId = 6 },
                new RespostaAvaliacao { Id = 32, Descricao = "1 hora de trabalho, e 8 horas de estudos diários", Correta = false, PerguntaAvaliacaoId = 6 },
                new RespostaAvaliacao { Id = 33, Descricao = "Aumente a sua renda à medida em que o seu padrão de vida aumentar", Correta = false, PerguntaAvaliacaoId = 7 },
                new RespostaAvaliacao { Id = 34, Descricao = "Aumente o seu padrão de vida somente após alcançar o objetivo definido em seu plano financeiro", Correta = true, PerguntaAvaliacaoId = 7 },
                new RespostaAvaliacao { Id = 35, Descricao = "Simplifique a sua vida simplesmente aumentando o seu padrão de vida", Correta = false, PerguntaAvaliacaoId = 7 },
                new RespostaAvaliacao { Id = 36, Descricao = "Simplifique a sua vida simplesmente aumentando a sua renda", Correta = false, PerguntaAvaliacaoId = 7 },
                new RespostaAvaliacao { Id = 37, Descricao = "Pagar a si mesmo", Correta = false, PerguntaAvaliacaoId = 8 },
                new RespostaAvaliacao { Id = 38, Descricao = "Pagar as dívidas", Correta = true, PerguntaAvaliacaoId = 8 },
                new RespostaAvaliacao { Id = 39, Descricao = "Poupar para a sua segurança financeira", Correta = false, PerguntaAvaliacaoId = 8 },
                new RespostaAvaliacao { Id = 40, Descricao = "Investir", Correta = false, PerguntaAvaliacaoId = 8 },
                new RespostaAvaliacao { Id = 41, Descricao = "Seguros", Correta = false, PerguntaAvaliacaoId = 8 },
                new RespostaAvaliacao { Id = 42, Descricao = "Ter um bom rendimento mensal", Correta = false, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 43, Descricao = "Saber diferenciar dívidas de contas mensais", Correta = false, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 44, Descricao = "Ser diligente", Correta = true, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 45, Descricao = "Ser uma pessoa inteligente", Correta = false, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 46, Descricao = "Ter uma aplicação específica para os estudos dos filhos", Correta = false, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 47, Descricao = "Investir em ativos de alta rentabilidade", Correta = false, PerguntaAvaliacaoId = 10 },
                new RespostaAvaliacao { Id = 48, Descricao = "Wallace D. Wattles", Correta = false, PerguntaAvaliacaoId = 11 },
                new RespostaAvaliacao { Id = 49, Descricao = "Bob Proctor", Correta = false, PerguntaAvaliacaoId = 11 },
                new RespostaAvaliacao { Id = 50, Descricao = "Paulo Vieira", Correta = false, PerguntaAvaliacaoId = 11 },
                new RespostaAvaliacao { Id = 51, Descricao = "Jim Rohn", Correta = true, PerguntaAvaliacaoId = 11 },
                new RespostaAvaliacao { Id = 52, Descricao = "Paul Mackenna", Correta = false, PerguntaAvaliacaoId = 11 },
                new RespostaAvaliacao { Id = 53, Descricao = "Especialista nível I: Ganha 3 vezes mais que um profissional comum", Correta = false, PerguntaAvaliacaoId = 12 },
                new RespostaAvaliacao { Id = 54, Descricao = "Especialista nível II: Ganha 6 vezes mais que um profissional comum", Correta = false, PerguntaAvaliacaoId = 12 },
                new RespostaAvaliacao { Id = 55, Descricao = "Especialista nível III: Ganha 8 vezes mais que um profissional comum", Correta = true, PerguntaAvaliacaoId = 12 },
                new RespostaAvaliacao { Id = 56, Descricao = "Especialista nível IV: Ganha 12 vezes mais que um profissional comum", Correta = false, PerguntaAvaliacaoId = 12 },
                new RespostaAvaliacao { Id = 57, Descricao = "Especialista nível V: Ganha 15 vezes mais que um profissional comum", Correta = false, PerguntaAvaliacaoId = 12 },
                new RespostaAvaliacao { Id = 58, Descricao = "Para formação de sua PROTEÇÃO FINANCEIRA: 6 salários para empregados; 12 salários para autônomos ou empreendedores", Correta = false, PerguntaAvaliacaoId = 13 },
                new RespostaAvaliacao { Id = 59, Descricao = "Para formação de sua SEGURANÇA FINANCEIRA: 120 salários", Correta = false, PerguntaAvaliacaoId = 13 },
                new RespostaAvaliacao { Id = 60, Descricao = "Para formação de sua LIBERDADE FINANCEIRA: 240 salários", Correta = true, PerguntaAvaliacaoId = 13 }

                );
        }
    }
}
