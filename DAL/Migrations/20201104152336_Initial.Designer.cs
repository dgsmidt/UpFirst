﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(UpFirstDbContext))]
    [Migration("20201104152336_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NotaQuestionario")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alunos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "teste@teste.com",
                            Nome = "Teste",
                            NotaQuestionario = 0m,
                            UserId = "Teste",
                            WhatsApp = "12999888877"
                        },
                        new
                        {
                            Id = 2,
                            Email = "claudio.rosa@gswsoftware.com",
                            Nome = "Cláudio",
                            NotaQuestionario = 4.5m,
                            UserId = "194ee88d-44fd-4168-b360-8da5c600726c"
                        },
                        new
                        {
                            Id = 4,
                            Email = "csrclaudio@gmail.com",
                            Nome = "Cláudio",
                            NotaQuestionario = 0m,
                            UserId = "7cb31e03-5a94-4527-b44d-a6791d20d842"
                        },
                        new
                        {
                            Id = 5,
                            Email = "marcileychristovao@uol.com.br",
                            Nome = "Marciley",
                            NotaQuestionario = 2.8m,
                            UserId = "966a4985-0049-405a-9685-38c37a03ca39"
                        },
                        new
                        {
                            Id = 6,
                            Email = "claudio_vilanova@yahoo.com.br",
                            Nome = "Cláudio",
                            NotaQuestionario = 0m,
                            UserId = "9adc3d2f-34f7-4c22-9ef2-2c19d8c8b7c4"
                        },
                        new
                        {
                            Id = 7,
                            Email = "daniel.smidt@yahoo.com.br",
                            Nome = "Daniel",
                            NotaQuestionario = 2.0m,
                            UserId = "cf1b9d7f-9881-4437-bcbb-0e32a6ec2525"
                        });
                });

            modelBuilder.Entity("DAL.Models.Aula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialApoio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroAula")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModuloId");

                    b.ToTable("Aulas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Aula 1",
                            ModuloId = 1,
                            NumeroAula = 1,
                            Video = "https://www.youtube.com/watch?v=eehO6YQycBQ"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Aula 2",
                            ModuloId = 1,
                            NumeroAula = 2,
                            Video = "https://www.youtube.com/watch?v=5niylfZuZ8k"
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Aula 3",
                            ModuloId = 1,
                            NumeroAula = 3,
                            Video = "https://www.youtube.com/watch?v=wHsG4G3evWE"
                        },
                        new
                        {
                            Id = 4,
                            Descricao = "Aula 4",
                            ModuloId = 1,
                            NumeroAula = 4,
                            Video = "https://www.youtube.com/watch?v=_DYno3fsLEw"
                        },
                        new
                        {
                            Id = 5,
                            Descricao = "Aula 1",
                            ModuloId = 2,
                            NumeroAula = 1,
                            Video = "https://www.youtube.com/watch?v=fnv-o1kFI6g"
                        },
                        new
                        {
                            Id = 6,
                            Descricao = "Aula 1",
                            ModuloId = 3,
                            NumeroAula = 1,
                            Video = "https://www.youtube.com/watch?v=PPbPy7BNvBs"
                        });
                });

            modelBuilder.Entity("DAL.Models.AulaAluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<string>("Anotacoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Assistida")
                        .HasColumnType("bit");

                    b.Property<bool>("Assistindo")
                        .HasColumnType("bit");

                    b.Property<int>("AulaId")
                        .HasColumnType("int");

                    b.Property<bool>("HabilitarAssistida")
                        .HasColumnType("bit");

                    b.Property<int?>("ModuloAlunoId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroAula")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("AulaId");

                    b.HasIndex("ModuloAlunoId");

                    b.ToTable("AulasAlunos");
                });

            modelBuilder.Entity("DAL.Models.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Avaliacoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Avaliação 1",
                            ModuloId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.Configuracao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CabecalhoTexto1_Index")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NotaDeCorte")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<string>("Texto1_Index")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configuracoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CabecalhoTexto1_Index = "Suas finanças de maneira inteligente",
                            Logo = "~/assets/images/upfirst_logo.svg",
                            NotaDeCorte = 0m,
                            Texto1_Index = "O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!"
                        });
                });

            modelBuilder.Entity("DAL.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.ToTable("Cursos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "FUNDAMENTAL",
                            Preco = 5.1m
                        },
                        new
                        {
                            Id = 2,
                            Nome = "EDUCAÇÃO FINANCEIRA",
                            Preco = 5.2m
                        },
                        new
                        {
                            Id = 3,
                            Nome = "INVESTIMENTOS",
                            Preco = 5.3m
                        });
                });

            modelBuilder.Entity("DAL.Models.CursoAluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Liberado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CursosAlunos");
                });

            modelBuilder.Entity("DAL.Models.MercadoPago_Ipn", b =>
                {
                    b.Property<int>("MercadoPago_IpnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MercadoPago_IpnId");

                    b.ToTable("MercadoPago_Ipns");
                });

            modelBuilder.Entity("DAL.Models.MercadoPago_WebHook", b =>
                {
                    b.Property<int>("MercadoPago_WebHookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<long>("DataId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MercadoPago_WebHookId");

                    b.ToTable("MercadoPago_WebHooks");
                });

            modelBuilder.Entity("DAL.Models.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AvaliacaoId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroModulo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId")
                        .IsUnique()
                        .HasFilter("[AvaliacaoId] IS NOT NULL");

                    b.HasIndex("CursoId");

                    b.ToTable("Modulos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvaliacaoId = 1,
                            CursoId = 1,
                            Descricao = "Modulo 1",
                            NumeroModulo = 1
                        },
                        new
                        {
                            Id = 2,
                            CursoId = 1,
                            Descricao = "Modulo 2",
                            NumeroModulo = 2
                        },
                        new
                        {
                            Id = 3,
                            CursoId = 2,
                            Descricao = "Modulo 1",
                            NumeroModulo = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.ModuloAluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<bool>("AvaliacaoLiberada")
                        .HasColumnType("bit");

                    b.Property<int?>("CursoAlunoId")
                        .HasColumnType("int");

                    b.Property<bool>("Liberado")
                        .HasColumnType("bit");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.Property<decimal>("Nota")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<int>("NumeroModulo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoAlunoId");

                    b.HasIndex("ModuloId");

                    b.ToTable("ModulosAlunos");
                });

            modelBuilder.Entity("DAL.Models.Pagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Forma")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoId");

                    b.ToTable("Pagamentos");
                });

            modelBuilder.Entity("DAL.Models.PerguntaAvaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvaliacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.ToTable("PerguntasAvaliacao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvaliacaoId = 1,
                            Descricao = "Quanto é 1 x 1 ?"
                        },
                        new
                        {
                            Id = 2,
                            AvaliacaoId = 1,
                            Descricao = "Quanto é 2 x 6 ?"
                        });
                });

            modelBuilder.Entity("DAL.Models.PerguntaQuestionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Resposta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionarioId");

                    b.ToTable("PerguntasQuestionario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "PLANEJAMENTO FINANCEIRO",
                            QuestionarioId = 1
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "CONHECE O QUE É BOLSA DE VALORES?",
                            QuestionarioId = 1
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "OUTRAS PERGUNTAS PARA O USUÁRIO",
                            QuestionarioId = 1
                        },
                        new
                        {
                            Id = 4,
                            Descricao = "MAIS UMA PERGUNTA AQUI",
                            QuestionarioId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.Questionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questionarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Questionário Inicial"
                        });
                });

            modelBuilder.Entity("DAL.Models.RespostaAvaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Correta")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PerguntaAvaliacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerguntaAvaliacaoId");

                    b.ToTable("RespostasAvaliacao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Correta = false,
                            Descricao = "Zero",
                            PerguntaAvaliacaoId = 1
                        },
                        new
                        {
                            Id = 2,
                            Correta = true,
                            Descricao = "Um",
                            PerguntaAvaliacaoId = 1
                        },
                        new
                        {
                            Id = 3,
                            Correta = false,
                            Descricao = "Dois",
                            PerguntaAvaliacaoId = 1
                        },
                        new
                        {
                            Id = 4,
                            Correta = false,
                            Descricao = "Três",
                            PerguntaAvaliacaoId = 1
                        },
                        new
                        {
                            Id = 5,
                            Correta = false,
                            Descricao = "6",
                            PerguntaAvaliacaoId = 2
                        },
                        new
                        {
                            Id = 6,
                            Correta = false,
                            Descricao = "13",
                            PerguntaAvaliacaoId = 2
                        },
                        new
                        {
                            Id = 7,
                            Correta = true,
                            Descricao = "12",
                            PerguntaAvaliacaoId = 2
                        },
                        new
                        {
                            Id = 8,
                            Correta = false,
                            Descricao = "14",
                            PerguntaAvaliacaoId = 2
                        });
                });

            modelBuilder.Entity("DAL.Models.Aula", b =>
                {
                    b.HasOne("DAL.Models.Modulo", "Modulo")
                        .WithMany("Aulas")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.AulaAluno", b =>
                {
                    b.HasOne("DAL.Models.Aluno", "Aluno")
                        .WithMany("AulasAlunos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Aula", "Aula")
                        .WithMany("AulasAlunos")
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.ModuloAluno", null)
                        .WithMany("AulasAlunos")
                        .HasForeignKey("ModuloAlunoId");
                });

            modelBuilder.Entity("DAL.Models.CursoAluno", b =>
                {
                    b.HasOne("DAL.Models.Aluno", "Aluno")
                        .WithMany("CursosAlunos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Curso", "Curso")
                        .WithMany("CursosAlunos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Modulo", b =>
                {
                    b.HasOne("DAL.Models.Avaliacao", "Avaliacao")
                        .WithOne("Modulo")
                        .HasForeignKey("DAL.Models.Modulo", "AvaliacaoId");

                    b.HasOne("DAL.Models.Curso", "Curso")
                        .WithMany("Modulos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.ModuloAluno", b =>
                {
                    b.HasOne("DAL.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.CursoAluno", null)
                        .WithMany("ModulosAlunos")
                        .HasForeignKey("CursoAlunoId");

                    b.HasOne("DAL.Models.Modulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Pagamento", b =>
                {
                    b.HasOne("DAL.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.PerguntaAvaliacao", b =>
                {
                    b.HasOne("DAL.Models.Avaliacao", "Avaliacao")
                        .WithMany("Perguntas")
                        .HasForeignKey("AvaliacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.PerguntaQuestionario", b =>
                {
                    b.HasOne("DAL.Models.Questionario", "Questionario")
                        .WithMany("Perguntas")
                        .HasForeignKey("QuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.RespostaAvaliacao", b =>
                {
                    b.HasOne("DAL.Models.PerguntaAvaliacao", "Pergunta")
                        .WithMany("Respostas")
                        .HasForeignKey("PerguntaAvaliacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}