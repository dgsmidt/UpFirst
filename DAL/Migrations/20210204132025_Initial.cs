using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    WhatsApp = table.Column<string>(nullable: true),
                    NotaQuestionario = table.Column<decimal>(type: "decimal(3, 1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuloId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailContato = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    CabecalhoTexto1_Index = table.Column<string>(nullable: true),
                    Texto1_Index = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Video_Index = table.Column<string>(nullable: true),
                    NotaDeCorte = table.Column<decimal>(type: "decimal(3, 1)", nullable: false),
                    CabecalhoTexto2_Index = table.Column<string>(nullable: true),
                    Texto2_Index = table.Column<string>(nullable: true),
                    CabecalhoTexto3_Index = table.Column<string>(nullable: true),
                    Texto3_Index = table.Column<string>(nullable: true),
                    TextoAlvo_Index = table.Column<string>(nullable: true),
                    TextoGrafico_Index = table.Column<string>(nullable: true),
                    TextoComputador_Index = table.Column<string>(nullable: true),
                    EnderecoLinha1 = table.Column<string>(nullable: true),
                    EnderecoLinha2 = table.Column<string>(nullable: true),
                    EnderecoLinha3 = table.Column<string>(nullable: true),
                    LogoBackground = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MercadoPago_WebHooks",
                columns: table => new
                {
                    MercadoPago_WebHookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    DataId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MercadoPago_WebHooks", x => x.MercadoPago_WebHookId);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Forma = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    PaymentId = table.Column<long>(nullable: false),
                    TipoPagamento = table.Column<string>(nullable: true),
                    StatusDetail = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerguntasAvaliacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvaliacaoId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntasAvaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerguntasAvaliacao_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    AvaliacaoId = table.Column<int>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    NumeroModulo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modulos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    PagamentoId = table.Column<int>(nullable: false),
                    Liberada = table.Column<bool>(nullable: false),
                    CursoConcluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerguntasQuestionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionarioId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Resposta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntasQuestionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerguntasQuestionario_Questionarios_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "Questionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasAvaliacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerguntaAvaliacaoId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Correta = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasAvaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasAvaliacao_PerguntasAvaliacao_PerguntaAvaliacaoId",
                        column: x => x.PerguntaAvaliacaoId,
                        principalTable: "PerguntasAvaliacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuloId = table.Column<int>(nullable: false),
                    NumeroAula = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true),
                    MaterialApoio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(nullable: false),
                    ModuloId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(3, 1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusAulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatriculaId = table.Column<int>(nullable: false),
                    AulaAssistindoId = table.Column<int>(nullable: false),
                    AulaPodeMarcarAssistidaId = table.Column<int>(nullable: false),
                    UltimoModuloLiberadoId = table.Column<int>(nullable: false),
                    AvaliacaoLiberadaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusAulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusAulas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnotacoesAulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(nullable: false),
                    AulaId = table.Column<int>(nullable: false),
                    Anotacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnotacoesAulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnotacoesAulas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnotacoesAulas_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArquivosApoio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivosApoio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivosApoio_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AulasAssistidas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    StatusAulasId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulasAssistidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AulasAssistidas_StatusAulas_StatusAulasId",
                        column: x => x.StatusAulasId,
                        principalTable: "StatusAulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Avaliacoes",
                columns: new[] { "Id", "Descricao", "ModuloId" },
                values: new object[,]
                {
                    { 1, "Avaliação 1", 1 },
                    { 2, "Modulo 1 - Prova", 1 },
                    { 3, "Modulo 2 - Prova", 1 },
                    { 4, "Modulo 3 - Prova", 1 },
                    { 5, "Modulo 4 - Prova", 1 },
                    { 6, "Modulo 5 - Prova", 1 }
                });

            migrationBuilder.InsertData(
                table: "Configuracoes",
                columns: new[] { "Id", "CabecalhoTexto1_Index", "CabecalhoTexto2_Index", "CabecalhoTexto3_Index", "EmailContato", "EnderecoLinha1", "EnderecoLinha2", "EnderecoLinha3", "Logo", "LogoBackground", "NotaDeCorte", "Texto1_Index", "Texto2_Index", "Texto3_Index", "TextoAlvo_Index", "TextoComputador_Index", "TextoGrafico_Index", "Titulo", "Video_Index" },
                values: new object[] { 1, "Suas finanças de maneira inteligente", "CONHEÇA NOSSOS CURSOS", "COMO FUNCIONA", "contato@upfirst.com.br", "Av. Dr. Nelson d''Ávila, 1837", "Centro - Sao Jose dos Campos", "", "/assets/images/upfirst_logo.svg", "RGB(6,26,55)", 5m, "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!", "Sucesso financeiro é ter dinheiro suficiente para fazer aquilo que você deseja, de forma planejada. Nossos cursos te dará a direção, e proporcionarão ferramentas para que você se torne uma pessoa financeiramente bem-sucedida, ter dinheiro suficiente para cobrir o seu custo de vida e realizar projetos futuros, sem precisar se endividar, por exemplo.", "Você aprenderá a prosperar financeiramente através de 5 bases simples: 1. Mudança de mentalidade financeira; 2. Poupar; 3. Investir; 4. Renda Extra; e 5. Simplificação. Você descobrirá oportunidades incríveis para aumentar as receitas, controlar as despesas, investir em ativos de alta rentabilidade, viver um estilo de vida simples e abundante! Quando você enriquece o mundo se torna um lugar melhor!", "A meta é a vida abundante! O que você não tem é pelo que você ainda não conhece. Você possui uma mente infinitamente criativa, uma capacidade infinita de ser uma pessoa rica. Só precisa aprender a usá-la.", "Pesquisas demonstram que 95% das pessoas se aponsentam com renda insuficiente na velhice, e dependem do governo ou de parentes. Milhões de brasileiros vivem essa realidade. Você pode planejar agora as finanças dos próximos anos e virar essa equação em seu favor. Te pegaremos pela mão e te auxiliaremos a planejar a sua reserva financeira para hoje e para o futuro.", "A parte mais trabalhosa de enriquecer é tomar deliberadamente a decisão de começar. Todas as demais etapas são bem mais simples. O que você precisa fazer agora é decidir se tornar uma pessoa próspera financeiramente e começar a agir!", "ESF", "/assets/videos/institucional.mp4" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descricao", "Nome", "Preco" },
                values: new object[] { 2, null, "EDUCAÇÃO FINANCEIRA", 399.00m });

            migrationBuilder.InsertData(
                table: "Questionarios",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Questionário Inicial" });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "AvaliacaoId", "CursoId", "Descricao", "NumeroModulo" },
                values: new object[,]
                {
                    { 7, 6, 2, "Modulo 5", 5 },
                    { 6, 5, 2, "Modulo 4", 4 },
                    { 5, 4, 2, "Modulo 3", 3 },
                    { 4, 3, 2, "Modulo 2", 2 },
                    { 3, 2, 2, "Modulo 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerguntasAvaliacao",
                columns: new[] { "Id", "AvaliacaoId", "Descricao" },
                values: new object[,]
                {
                    { 1, 1, "Quanto é 1 x 1 ?" },
                    { 13, 6, "A quantidade de salários sugeridos que você ganha, para compor a sua reserva financeira é?" },
                    { 7, 6, "Marque a única alternativa correta. Na aula sobre SIMPLIFICAÇÃO, ensinamos o seguinte conceito:" },
                    { 12, 5, "A média de ganho de um especialista é?" },
                    { 11, 4, "De quem é a célebre frase mencionada na aula 1 deste módulo 3: “Para que as coisas mudem, você tem que mudar.... Para que as coisas melhorem, você tem que melhorar.... Podemos ter mais do que já temos, porque podemos nos tornar melhores do que somos.”." },
                    { 5, 4, "Marque as 3 alternativas corretas. Riqueza verdadeira é aquela que combina as três dimensões humanas, quais são elas?" },
                    { 10, 3, "De acordo com a aula 3 deste módulo 2, o segredo dos vencedores é?" },
                    { 4, 3, "Marque a única alternativa incorreta. As 6 leis da autorresponsabilidade financeira são?" },
                    { 8, 2, "Qual alternativa não faz parte dos 11 pilares do MAF – Mapa de Autoavaliação Financeira?" },
                    { 3, 2, "Qual alternativa não faz parte das 5 bases deste curso?" },
                    { 2, 1, "Quanto é 2 x 6 ?" },
                    { 6, 5, "Na aula sobre construção de RENDA EXTRA, ensinamos que para se tornar um especialista em sua área de atuação é preciso concluir um macrociclo de 1.000 horas. O macrociclo é formado por 22 semanas de microciclos aplicados. A pergunta é: um microciclo de 45 horas semanais é composto por?" }
                });

            migrationBuilder.InsertData(
                table: "PerguntasQuestionario",
                columns: new[] { "Id", "Descricao", "QuestionarioId", "Resposta" },
                values: new object[,]
                {
                    { 8, "Qual seu conhecimento em investimentos?", 1, null },
                    { 7, "Qual seu conhecimento em despesas e receitas?", 1, null },
                    { 9, "Qual o seu conhecimento em controle financeiro?", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Aulas",
                columns: new[] { "Id", "Descricao", "MaterialApoio", "ModuloId", "NumeroAula", "Video" },
                values: new object[,]
                {
                    { 10, "Aula 1", null, 7, 1, "/uploads/Mod 05 - Aula 1.mp4" },
                    { 6, "Aula 1", null, 3, 1, "/uploads/Aula_1.mp4" },
                    { 11, "Aula 2", null, 3, 2, "/uploads/Aula 2.mp4" },
                    { 12, "Aula 3", null, 3, 3, "/uploads/Aula 3.mp4" },
                    { 13, "Aula 4", null, 3, 4, "/uploads/Aula 4.mp4" },
                    { 14, "Aula 5", null, 3, 5, "/uploads/Aula 5.mp4" },
                    { 9, "Aula 1", null, 6, 1, "/uploads/Mod 04 - Aula 1.mp4" },
                    { 15, "Aula 2", null, 4, 2, "/uploads/Mod 02 - Aula 2.mp4" },
                    { 16, "Aula 3", null, 4, 3, "/uploads/Mod 02 - Aula 3.mp4" },
                    { 17, "Aula 4", null, 4, 4, "/uploads/Mod 02 - Aula 4.mp4" },
                    { 8, "Aula 1", null, 5, 1, "/uploads/Mod 03 - Aula 1.mp4" },
                    { 18, "Aula 2", null, 5, 2, "/uploads/Mod 03 - Aula 2.mp4" },
                    { 7, "Aula 1", null, 4, 1, "/uploads/Mod 02 - Aula 1.mp4" }
                });

            migrationBuilder.InsertData(
                table: "RespostasAvaliacao",
                columns: new[] { "Id", "Correta", "Descricao", "PerguntaAvaliacaoId" },
                values: new object[,]
                {
                    { 49, false, "Bob Proctor", 11 },
                    { 50, false, "Paulo Vieira", 11 },
                    { 51, true, "Jim Rohn", 11 },
                    { 52, false, "Paul Mackenna", 11 },
                    { 12, false, "7 horas de trabalho, e 2 horas de estudos diários", 6 },
                    { 29, false, "6 horas de trabalho, e 3 horas de estudos diários", 6 },
                    { 30, false, "9 horas de trabalho, e 1 hora de estudos diários", 6 },
                    { 31, true, "8 horas de trabalho, e 1 hora de estudos diários", 6 },
                    { 32, false, "1 hora de trabalho, e 8 horas de estudos diários", 6 },
                    { 53, false, "Especialista nível I: Ganha 3 vezes mais que um profissional comum", 12 },
                    { 56, false, "Especialista nível IV: Ganha 12 vezes mais que um profissional comum", 12 },
                    { 55, true, "Especialista nível III: Ganha 8 vezes mais que um profissional comum", 12 },
                    { 57, false, "Especialista nível V: Ganha 15 vezes mais que um profissional comum", 12 },
                    { 13, false, "Aumente o seu padrão de vida à medida em que sua renda aumentar", 7 },
                    { 33, false, "Aumente a sua renda à medida em que o seu padrão de vida aumentar", 7 },
                    { 48, false, "Wallace D. Wattles", 11 },
                    { 35, false, "Simplifique a sua vida simplesmente aumentando o seu padrão de vida", 7 },
                    { 36, false, "Simplifique a sua vida simplesmente aumentando a sua renda", 7 },
                    { 58, false, "Para formação de sua PROTEÇÃO FINANCEIRA: 6 salários para empregados; 12 salários para autônomos ou empreendedores", 13 },
                    { 59, false, "Para formação de sua SEGURANÇA FINANCEIRA: 120 salários", 13 },
                    { 60, true, "Para formação de sua LIBERDADE FINANCEIRA: 240 salários", 13 },
                    { 54, false, "Especialista nível II: Ganha 6 vezes mais que um profissional comum", 12 },
                    { 34, true, "Aumente o seu padrão de vida somente após alcançar o objetivo definido em seu plano financeiro", 7 },
                    { 28, false, "Dimensão do TRABALHO, caracterizado pela CRENÇA DE PRODUZIR", 5 },
                    { 26, true, "Dimensão do TER, caracterizado pela CRENÇA DE MERECIMENTO", 5 },
                    { 37, false, "Pagar a si mesmo", 8 },
                    { 18, true, "Gastar", 3 },
                    { 17, false, "Simplificação", 3 },
                    { 16, false, "Renda extra", 3 },
                    { 15, false, "Investir", 3 },
                    { 14, false, "Poupar", 3 },
                    { 38, true, "Pagar as dívidas", 8 },
                    { 9, false, "Mudança de mentalidade financeira", 3 },
                    { 7, true, "12", 2 },
                    { 6, false, "13", 2 },
                    { 5, false, "6", 2 },
                    { 4, false, "Três", 1 },
                    { 3, false, "Dois", 1 },
                    { 2, true, "Um", 1 },
                    { 8, false, "14", 2 },
                    { 39, false, "Poupar para a sua segurança financeira", 8 },
                    { 40, false, "Investir", 8 },
                    { 41, false, "Seguros", 8 },
                    { 25, true, "Dimensão do FAZER, caracterizado pela CRENÇA DE CAPACIDADE", 5 },
                    { 11, true, "Dimensão do SER, caracterizado pela CRENÇA DE IDENTIDADE", 5 },
                    { 47, false, "Investir em ativos de alta rentabilidade", 10 },
                    { 46, false, "Ter uma aplicação específica para os estudos dos filhos", 10 },
                    { 45, false, "Ser uma pessoa inteligente", 10 },
                    { 44, true, "Ser diligente", 10 },
                    { 43, false, "Saber diferenciar dívidas de contas mensais", 10 },
                    { 42, false, "Ter um bom rendimento mensal", 10 },
                    { 24, false, "Se for julgar alguém, julgue a atitude dessa pessoa", 4 },
                    { 23, false, "Se for justificar os seus erros, aprenda com eles", 4 },
                    { 22, false, "Se for se fazer de vítima, faça-se de vencedor", 4 },
                    { 21, false, "Se for buscar culpados, busque a solução", 4 },
                    { 20, true, "Se for investir, invista na caderneta de poupança", 4 },
                    { 19, false, "Se for reclamar das circunstâncias, dê sugestão", 4 },
                    { 10, false, "Se for criticar as pessoas, cale-se", 4 },
                    { 27, false, "Dimensão da ECONOMIA, caracterizado pela CRENÇA DE POUPAR", 5 },
                    { 1, false, "Zero", 1 }
                });

            migrationBuilder.InsertData(
                table: "ArquivosApoio",
                columns: new[] { "Id", "AulaId", "FileName" },
                values: new object[,]
                {
                    { 1, 11, "/uploads/1 E se ... Modulo_1_aula_2.pdf" },
                    { 2, 11, "/uploads/2 Questionário Financeiro_Modulo_1_aula_2.pdf" },
                    { 3, 12, "/uploads/1 Anamnese Financeira_Modulo_1_aula_3.pdf" },
                    { 4, 13, "/uploads/Parâmetros do MAF_Modulo_1_aula_4.pdf" },
                    { 13, 14, "/uploads/Caderno de Exercícios__Modulo_1_aula_5.pdf" },
                    { 5, 7, "/uploads/Caderno de exercício__Modulo_2_aula_1.pdf" },
                    { 6, 15, "/uploads/Caderno de exercícios_Modulo_2_aula_2.pdf" },
                    { 14, 16, "/uploads/Caderno de exercício_Modulo_2_aula_3.pdf" },
                    { 15, 17, "/uploads/Caderno de exercício_Modulo_2_aula_4.pdf" },
                    { 9, 8, "/uploads/Pirâmide do Indivíduo_Modulo_3_aula_1.pdf" },
                    { 16, 18, "/uploads/Caderno de exercício_Modulo_3_aula_2.pdf" },
                    { 17, 18, "/uploads/Planilha Negociação Dívidas_Modulo_3_aula_2.pdf" },
                    { 18, 9, "/uploads/Agenda pag 1_Modulo_4_aula_1.pdf" },
                    { 19, 9, "/uploads/Agenda pag 2_Modulo_4_aula_1.pdf" },
                    { 20, 9, "/uploads/Caderno de exercícios_Modulo_4_aula_1.pdf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnotacoesAulas_AlunoId",
                table: "AnotacoesAulas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnotacoesAulas_AulaId",
                table: "AnotacoesAulas",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivosApoio_AulaId",
                table: "ArquivosApoio",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId",
                table: "Aulas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_AulasAssistidas_StatusAulasId",
                table: "AulasAssistidas",
                column: "StatusAulasId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_PagamentoId",
                table: "Matriculas",
                column: "PagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_AvaliacaoId",
                table: "Modulos",
                column: "AvaliacaoId",
                unique: true,
                filter: "[AvaliacaoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_CursoId",
                table: "Modulos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_AlunoId",
                table: "Notas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_ModuloId",
                table: "Notas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntasAvaliacao_AvaliacaoId",
                table: "PerguntasAvaliacao",
                column: "AvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntasQuestionario_QuestionarioId",
                table: "PerguntasQuestionario",
                column: "QuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAvaliacao_PerguntaAvaliacaoId",
                table: "RespostasAvaliacao",
                column: "PerguntaAvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusAulas_MatriculaId",
                table: "StatusAulas",
                column: "MatriculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotacoesAulas");

            migrationBuilder.DropTable(
                name: "ArquivosApoio");

            migrationBuilder.DropTable(
                name: "AulasAssistidas");

            migrationBuilder.DropTable(
                name: "Configuracoes");

            migrationBuilder.DropTable(
                name: "MercadoPago_WebHooks");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "PerguntasQuestionario");

            migrationBuilder.DropTable(
                name: "RespostasAvaliacao");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "StatusAulas");

            migrationBuilder.DropTable(
                name: "Questionarios");

            migrationBuilder.DropTable(
                name: "PerguntasAvaliacao");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
