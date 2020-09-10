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
                    NotaQuestionario = table.Column<decimal>(type: "decimal(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabecalhoTexto1_Index = table.Column<string>(nullable: true),
                    Texto1_Index = table.Column<string>(nullable: true)
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
                name: "MercadoPago_Ipns",
                columns: table => new
                {
                    Data = table.Column<DateTime>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
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
                name: "CursosAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    Liberado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursosAlunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosAlunos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
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
                    Descricao = table.Column<string>(nullable: true),
                    NumeroModulo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
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
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuloId = table.Column<int>(nullable: false),
                    NumeroAula = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Modulos_ModuloId",
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
                    Valor = table.Column<decimal>(type: "decimal(3,1)", nullable: false)
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
                name: "AulasAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    Assistida = table.Column<bool>(nullable: false),
                    Assistindo = table.Column<bool>(nullable: false),
                    Anotacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulasAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AulasAlunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AulasAlunos_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "NotaQuestionario", "UserId" },
                values: new object[] { 1, 0m, "Teste" });

            migrationBuilder.InsertData(
                table: "Configuracoes",
                columns: new[] { "Id", "CabecalhoTexto1_Index", "Texto1_Index" },
                values: new object[] { 1, "Suas finanças de maneira inteligente", "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descricao", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, null, "Curso 1", 0m },
                    { 2, null, "Curso 2", 0m }
                });

            migrationBuilder.InsertData(
                table: "Questionarios",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Questionário Inicial" });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "CursoId", "Descricao", "NumeroModulo" },
                values: new object[,]
                {
                    { 1, 1, "Modulo 1", 1 },
                    { 2, 1, "Modulo 2", 2 },
                    { 3, 2, "Modulo 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerguntasQuestionario",
                columns: new[] { "Id", "Descricao", "QuestionarioId", "Resposta" },
                values: new object[,]
                {
                    { 1, "PLANEJAMENTO FINANCEIRO", 1, null },
                    { 2, "CONHECE O QUE É BOLSA DE VALORES?", 1, null },
                    { 3, "OUTRAS PERGUNTAS PARA O USUÁRIO", 1, null },
                    { 4, "MAIS UMA PERGUNTA AQUI", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Aulas",
                columns: new[] { "Id", "Descricao", "ModuloId", "NumeroAula", "Video" },
                values: new object[,]
                {
                    { 1, "Aula 1", 1, 1, "https://player.vimeo.com/video/141439971" },
                    { 2, "Aula 2", 1, 2, "https://player.vimeo.com/video/141561250" },
                    { 3, "Aula 3", 1, 3, "https://player.vimeo.com/video/444387842" },
                    { 4, "Aula 4", 1, 4, "https://player.vimeo.com/video/116629498" },
                    { 5, "Aula 1", 2, 1, "https://player.vimeo.com/video/436144408" },
                    { 6, "Aula 1", 3, 1, "https://player.vimeo.com/video/116619880" }
                });

            migrationBuilder.InsertData(
                table: "Avaliacoes",
                columns: new[] { "Id", "Descricao", "ModuloId" },
                values: new object[] { 1, "Avaliação 1", 1 });

            migrationBuilder.InsertData(
                table: "PerguntasAvaliacao",
                columns: new[] { "Id", "AvaliacaoId", "Descricao" },
                values: new object[] { 1, 1, "Quanto é 1 x 1 ?" });

            migrationBuilder.InsertData(
                table: "PerguntasAvaliacao",
                columns: new[] { "Id", "AvaliacaoId", "Descricao" },
                values: new object[] { 2, 1, "Quanto é 2 x 6 ?" });

            migrationBuilder.InsertData(
                table: "RespostasAvaliacao",
                columns: new[] { "Id", "Correta", "Descricao", "PerguntaAvaliacaoId" },
                values: new object[,]
                {
                    { 1, false, "Zero", 1 },
                    { 2, true, "Um", 1 },
                    { 3, false, "Dois", 1 },
                    { 4, false, "Três", 1 },
                    { 5, false, "6", 2 },
                    { 6, false, "13", 2 },
                    { 7, true, "12", 2 },
                    { 8, false, "14", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId",
                table: "Aulas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_AulasAlunos_AlunoId",
                table: "AulasAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AulasAlunos_AulaId",
                table: "AulasAlunos",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_ModuloId",
                table: "Avaliacoes",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAlunos_AlunoId",
                table: "CursosAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAlunos_CursoId",
                table: "CursosAlunos",
                column: "CursoId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AulasAlunos");

            migrationBuilder.DropTable(
                name: "Configuracoes");

            migrationBuilder.DropTable(
                name: "CursosAlunos");

            migrationBuilder.DropTable(
                name: "MercadoPago_Ipns");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "PerguntasQuestionario");

            migrationBuilder.DropTable(
                name: "RespostasAvaliacao");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Questionarios");

            migrationBuilder.DropTable(
                name: "PerguntasAvaliacao");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
