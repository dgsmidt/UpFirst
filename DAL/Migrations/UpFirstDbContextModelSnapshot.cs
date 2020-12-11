﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(UpFirstDbContext))]
    partial class UpFirstDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("NotaQuestionario")
                        .HasColumnType("decimal(3,1)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alunos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NotaQuestionario = 0m,
                            UserId = "Teste"
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
                            Video = "https://player.vimeo.com/video/141439971"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Aula 2",
                            ModuloId = 1,
                            NumeroAula = 2,
                            Video = "https://player.vimeo.com/video/141561250"
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Aula 3",
                            ModuloId = 1,
                            NumeroAula = 3,
                            Video = "https://player.vimeo.com/video/444387842"
                        },
                        new
                        {
                            Id = 4,
                            Descricao = "Aula 4",
                            ModuloId = 1,
                            NumeroAula = 4,
                            Video = "https://player.vimeo.com/video/116629498"
                        },
                        new
                        {
                            Id = 5,
                            Descricao = "Aula 1",
                            ModuloId = 2,
                            NumeroAula = 1,
                            Video = "https://player.vimeo.com/video/436144408"
                        },
                        new
                        {
                            Id = 6,
                            Descricao = "Aula 1",
                            ModuloId = 3,
                            NumeroAula = 1,
                            Video = "https://player.vimeo.com/video/116619880"
                        });
                });

            modelBuilder.Entity("DAL.Models.AulasAlunos", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("AulaId");

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

                    b.HasIndex("ModuloId");

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

                    b.Property<string>("Texto1_Index")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configuracoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CabecalhoTexto1_Index = "Suas finanças de maneira inteligente",
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
                            Preco = 162.67m
                        },
                        new
                        {
                            Id = 2,
                            Nome = "EDUCAÇÃO FINANCEIRA",
                            Preco = 84.67m
                        },
                        new
                        {
                            Id = 3,
                            Nome = "INVESTIMENTOS",
                            Preco = 172.67m
                        });
                });

            modelBuilder.Entity("DAL.Models.CursosAlunos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("DAL.Models.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroModulo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Modulos");

                    b.HasData(
                        new
                        {
                            Id = 1,
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

            modelBuilder.Entity("DAL.Models.Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("ModuloId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(3,1)");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("ModuloId");

                    b.ToTable("Notas");
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

            modelBuilder.Entity("DAL.Models.AulasAlunos", b =>
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
                });

            modelBuilder.Entity("DAL.Models.Avaliacao", b =>
                {
                    b.HasOne("DAL.Models.Modulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.CursosAlunos", b =>
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
                    b.HasOne("DAL.Models.Curso", "Curso")
                        .WithMany("Modulos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Nota", b =>
                {
                    b.HasOne("DAL.Models.Aluno", "Aluno")
                        .WithMany("Notas")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Modulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloId")
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