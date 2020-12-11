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
                    CabecalhoTexto1_Index = table.Column<string>(nullable: true),
                    Texto1_Index = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    NotaDeCorte = table.Column<decimal>(type: "decimal(3, 1)", nullable: false)
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
                    MercadoPago_IpnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MercadoPago_Ipns", x => x.MercadoPago_IpnId);
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
                name: "CursosAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
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
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Pagamentos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Cursos_CursoId",
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
                name: "ModulosAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(nullable: false),
                    ModuloId = table.Column<int>(nullable: false),
                    Nota = table.Column<decimal>(type: "decimal(3, 1)", nullable: false),
                    Liberado = table.Column<bool>(nullable: false),
                    AvaliacaoLiberada = table.Column<bool>(nullable: false),
                    NumeroModulo = table.Column<int>(nullable: false),
                    CursoAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulosAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulosAlunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModulosAlunos_CursosAlunos_CursoAlunoId",
                        column: x => x.CursoAlunoId,
                        principalTable: "CursosAlunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModulosAlunos_Modulos_ModuloId",
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
                    Anotacoes = table.Column<string>(nullable: true),
                    HabilitarAssistida = table.Column<bool>(nullable: false),
                    NumeroAula = table.Column<int>(nullable: false),
                    ModuloAlunoId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_AulasAlunos_ModulosAlunos_ModuloAlunoId",
                        column: x => x.ModuloAlunoId,
                        principalTable: "ModulosAlunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Email", "Nome", "NotaQuestionario", "UserId", "WhatsApp" },
                values: new object[,]
                {
                    { 1, "teste@teste.com", "Teste", 0m, "Teste", "12999888877" },
                    { 2, "claudio.rosa@gswsoftware.com", "Cláudio", 4.5m, "194ee88d-44fd-4168-b360-8da5c600726c", null },
                    { 4, "csrclaudio@gmail.com", "Cláudio", 0m, "7cb31e03-5a94-4527-b44d-a6791d20d842", null },
                    { 5, "marcileychristovao@uol.com.br", "Marciley", 2.8m, "966a4985-0049-405a-9685-38c37a03ca39", null },
                    { 6, "claudio_vilanova@yahoo.com.br", "Cláudio", 0m, "9adc3d2f-34f7-4c22-9ef2-2c19d8c8b7c4", null },
                    { 7, "daniel.smidt@yahoo.com.br", "Daniel", 2.0m, "cf1b9d7f-9881-4437-bcbb-0e32a6ec2525", null }
                });

            migrationBuilder.InsertData(
                table: "Avaliacoes",
                columns: new[] { "Id", "Descricao", "ModuloId" },
                values: new object[] { 1, "Avaliação 1", 1 });

            migrationBuilder.InsertData(
                table: "Configuracoes",
                columns: new[] { "Id", "CabecalhoTexto1_Index", "Logo", "NotaDeCorte", "Texto1_Index" },
                values: new object[] { 1, "Suas finanças de maneira inteligente", "~/assets/images/upfirst_logo.svg", 0m, "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descricao", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, null, "FUNDAMENTAL", 5.1m },
                    { 2, null, "EDUCAÇÃO FINANCEIRA", 5.2m },
                    { 3, null, "INVESTIMENTOS", 5.3m }
                });

            migrationBuilder.InsertData(
                table: "Questionarios",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Questionário Inicial" });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "AvaliacaoId", "CursoId", "Descricao", "NumeroModulo" },
                values: new object[,]
                {
                    { 1, 1, 1, "Modulo 1", 1 },
                    { 2, null, 1, "Modulo 2", 2 },
                    { 3, null, 2, "Modulo 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "PerguntasAvaliacao",
                columns: new[] { "Id", "AvaliacaoId", "Descricao" },
                values: new object[,]
                {
                    { 1, 1, "Quanto é 1 x 1 ?" },
                    { 2, 1, "Quanto é 2 x 6 ?" }
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
                columns: new[] { "Id", "Descricao", "MaterialApoio", "ModuloId", "NumeroAula", "Video" },
                values: new object[,]
                {
                    { 1, "Aula 1", null, 1, 1, "https://www.youtube.com/watch?v=eehO6YQycBQ" },
                    { 2, "Aula 2", null, 1, 2, "https://www.youtube.com/watch?v=5niylfZuZ8k" },
                    { 3, "Aula 3", null, 1, 3, "https://www.youtube.com/watch?v=wHsG4G3evWE" },
                    { 4, "Aula 4", null, 1, 4, "https://www.youtube.com/watch?v=_DYno3fsLEw" },
                    { 5, "Aula 1", null, 2, 1, "https://www.youtube.com/watch?v=fnv-o1kFI6g" },
                    { 6, "Aula 1", null, 3, 1, "https://www.youtube.com/watch?v=PPbPy7BNvBs" }
                });

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
                name: "IX_AulasAlunos_ModuloAlunoId",
                table: "AulasAlunos",
                column: "ModuloAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAlunos_AlunoId",
                table: "CursosAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAlunos_CursoId",
                table: "CursosAlunos",
                column: "CursoId");

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
                name: "IX_ModulosAlunos_AlunoId",
                table: "ModulosAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulosAlunos_CursoAlunoId",
                table: "ModulosAlunos",
                column: "CursoAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulosAlunos_ModuloId",
                table: "ModulosAlunos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_AlunoId",
                table: "Pagamentos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_CursoId",
                table: "Pagamentos",
                column: "CursoId");

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
                name: "MercadoPago_Ipns");

            migrationBuilder.DropTable(
                name: "MercadoPago_WebHooks");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "PerguntasQuestionario");

            migrationBuilder.DropTable(
                name: "RespostasAvaliacao");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "ModulosAlunos");

            migrationBuilder.DropTable(
                name: "Questionarios");

            migrationBuilder.DropTable(
                name: "PerguntasAvaliacao");

            migrationBuilder.DropTable(
                name: "CursosAlunos");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
