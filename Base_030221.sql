/*
Navicat SQL Server Data Transfer

Source Server         : Mochahost
Source Server Version : 130000
Source Host           : 198.38.83.200:1433
Source Database       : ublack_upfirst_esf
Source Schema         : ublack_daniel

Target Server Type    : SQL Server
Target Server Version : 130000
File Encoding         : 65001

Date: 2021-02-03 18:11:23
*/


-- ----------------------------
-- Table structure for [Alunos]
-- ----------------------------
DROP TABLE [Alunos]
GO
CREATE TABLE [Alunos] (
[Id] int NOT NULL IDENTITY(1,1) ,
[UserId] nvarchar(MAX) NULL ,
[Nome] nvarchar(MAX) NULL ,
[Email] nvarchar(MAX) NULL ,
[WhatsApp] nvarchar(MAX) NULL ,
[NotaQuestionario] decimal(3,1) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[Alunos]', RESEED, 19)
GO

-- ----------------------------
-- Records of Alunos
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Alunos] ON
GO
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'1', N'Teste', N'Teste', N'teste@teste.com', N'12999888877', N'.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'4', N'7cb31e03-5a94-4527-b44d-a6791d20d842', N'Cláudio', N'csrclaudio@gmail.com', null, N'1.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'6', N'9adc3d2f-34f7-4c22-9ef2-2c19d8c8b7c4', N'Cláudio', N'claudio_vilanova@yahoo.com.br', null, N'1.5');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'7', N'cf1b9d7f-9881-4437-bcbb-0e32a6ec2525', N'Daniel', N'daniel.smidt@yahoo.com.br', null, N'2.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'9', N'd668909a-0148-4e01-95c3-6b36f11f18bb', N'Admin', N'admin@admin', null, N'1.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'10', N'8b80b7e5-a21d-432a-8b8d-a95d2dbbc895', N'Brenda', N'brendacnrosa@gmail.com', null, N'2.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'11', N'2d317929-860b-4720-9619-ba4b664555bf', N'Escola de Sucesso', N'esf@escoladesucessofinanceiro.com', null, N'.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'13', N'6966e040-a37f-4333-9158-48f4d09c07a5', N'Emily Cristina', N'emilyneves.cristina@gmail.com', null, N'1.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'14', N'd11114b0-05fa-41f2-a044-e5bd1bf33134', N'Adelson ', N'adelsoncoach@gmail.com', null, N'1.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'15', N'c8d44755-7c34-4071-b2a9-61465ea5b202', N'Claudio Rosa', N'claudiosilvarosa@hotmail.com', N'12997718964', N'2.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'17', N'ebf7a769-f6c8-4c7d-8137-a80bb0329b42', N'Claudio Rosa', N'claudio.rosa@gswsoftware.com', N'12997718964', N'.0');
INSERT INTO [Alunos] ([Id], [UserId], [Nome], [Email], [WhatsApp], [NotaQuestionario]) VALUES (N'19', N'c4edaf30-275e-4242-846d-8d90e5f4e6dd', N'Daniel', N'daniel.gsmidt@gmail.com', null, N'.0');
GO
SET IDENTITY_INSERT [Alunos] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [ArquivosApoio]
-- ----------------------------
DROP TABLE [ArquivosApoio]
GO
CREATE TABLE [ArquivosApoio] (
[Id] int NOT NULL IDENTITY(1,1) ,
[AulaId] int NOT NULL ,
[FileName] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[ArquivosApoio]', RESEED, 20)
GO

-- ----------------------------
-- Records of ArquivosApoio
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [ArquivosApoio] ON
GO
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'1', N'11', N'/uploads/1 E se ... Modulo_1_aula_2.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'2', N'11', N'/uploads/2 Questionário Financeiro_Modulo_1_aula_2.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'3', N'12', N'/uploads/1 Anamnese Financeira_Modulo_1_aula_3.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'4', N'13', N'/uploads/Parâmetros do MAF_Modulo_1_aula_4.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'5', N'7', N'/uploads/Caderno de exercício__Modulo_2_aula_1.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'6', N'15', N'/uploads/Caderno de exercícios_Modulo_2_aula_2.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'9', N'8', N'/uploads/Pirâmide do Indivíduo_Modulo_3_aula_1.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'13', N'14', N'/uploads/Caderno de Exercícios__Modulo_1_aula_5.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'14', N'16', N'/uploads/Caderno de exercício_Modulo_2_aula_3.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'15', N'17', N'/uploads/Caderno de exercício_Modulo_2_aula_4.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'16', N'18', N'/uploads/Caderno de exercício_Modulo_3_aula_2.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'17', N'18', N'/uploads/Planilha Negociação Dívidas_Modulo_3_aula_2.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'18', N'9', N'/uploads/Agenda pag 1_Modulo_4_aula_1.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'19', N'9', N'/uploads/Agenda pag 2_Modulo_4_aula_1.pdf');
INSERT INTO [ArquivosApoio] ([Id], [AulaId], [FileName]) VALUES (N'20', N'9', N'/uploads/Caderno de exercícios_Modulo_4_aula_1.pdf');
GO
SET IDENTITY_INSERT [ArquivosApoio] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Aulas]
-- ----------------------------
DROP TABLE [Aulas]
GO
CREATE TABLE [Aulas] (
[Id] int NOT NULL IDENTITY(1,1) ,
[ModuloId] int NOT NULL ,
[NumeroAula] int NOT NULL ,
[Descricao] nvarchar(MAX) NULL ,
[Video] nvarchar(MAX) NULL ,
[MaterialApoio] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[Aulas]', RESEED, 18)
GO

-- ----------------------------
-- Records of Aulas
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Aulas] ON
GO
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'6', N'3', N'1', N'Aula 1', N'/uploads/Aula_1.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'7', N'4', N'1', N'Aula 1', N'/uploads/Mod 02 - Aula 1.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'8', N'5', N'1', N'Aula 1', N'/uploads/Mod 03 - Aula 1.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'9', N'6', N'1', N'Aula 1', N'/uploads/Mod 04 - Aula 1.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'10', N'7', N'1', N'Aula 1', N'/uploads/Mod 05 - Aula 1.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'11', N'3', N'2', N'Aula 2', N'/uploads/Aula 2.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'12', N'3', N'3', N'Aula 3', N'/uploads/Aula 3.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'13', N'3', N'4', N'Aula 4', N'/uploads/Aula 4.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'14', N'3', N'5', N'Aula 5', N'/uploads/Aula 5.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'15', N'4', N'2', N'Aula 2', N'/uploads/Mod 02 - Aula 2.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'16', N'4', N'3', N'Aula 3', N'/uploads/Mod 02 - Aula 3.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'17', N'4', N'4', N'Aula 4', N'/uploads/Mod 02 - Aula 4.mp4', null);
INSERT INTO [Aulas] ([Id], [ModuloId], [NumeroAula], [Descricao], [Video], [MaterialApoio]) VALUES (N'18', N'5', N'2', N'Aula 2', N'/uploads/Mod 03 - Aula 2.mp4', null);
GO
SET IDENTITY_INSERT [Aulas] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Avaliacoes]
-- ----------------------------
DROP TABLE [Avaliacoes]
GO
CREATE TABLE [Avaliacoes] (
[Id] int NOT NULL IDENTITY(1,1) ,
[ModuloId] int NOT NULL ,
[Descricao] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[Avaliacoes]', RESEED, 6)
GO

-- ----------------------------
-- Records of Avaliacoes
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Avaliacoes] ON
GO
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'1', N'1', N'Avaliação 1');
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'2', N'3', N'Modulo 1 - Prova');
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'3', N'4', N'Modulo 2 - Prova');
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'4', N'5', N'Modulo 3 - Prova');
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'5', N'6', N'Modulo 4 - Prova');
INSERT INTO [Avaliacoes] ([Id], [ModuloId], [Descricao]) VALUES (N'6', N'7', N'Modulo 5 - Prova');
GO
SET IDENTITY_INSERT [Avaliacoes] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Configuracoes]
-- ----------------------------
DROP TABLE [Configuracoes]
GO
CREATE TABLE [Configuracoes] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Titulo] nvarchar(MAX) NULL ,
[CabecalhoTexto1_Index] nvarchar(MAX) NULL ,
[Texto1_Index] nvarchar(MAX) NULL ,
[Logo] nvarchar(MAX) NULL ,
[Video_Index] nvarchar(MAX) NULL ,
[NotaDeCorte] decimal(3,1) NOT NULL ,
[CabecalhoTexto2_Index] nvarchar(MAX) NULL ,
[Texto2_Index] nvarchar(MAX) NULL ,
[CabecalhoTexto3_Index] nvarchar(MAX) NULL ,
[Texto3_Index] nvarchar(MAX) NULL ,
[TextoAlvo_Index] nvarchar(MAX) NULL ,
[TextoGrafico_Index] nvarchar(MAX) NULL ,
[TextoComputador_Index] nvarchar(MAX) NULL ,
[EnderecoLinha1] nvarchar(MAX) NULL ,
[EnderecoLinha2] nvarchar(MAX) NULL ,
[EnderecoLinha3] nvarchar(MAX) NULL ,
[EmailContato] nvarchar(MAX) NULL ,
[LogoBackground] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of Configuracoes
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Configuracoes] ON
GO
INSERT INTO [Configuracoes] ([Id], [Titulo], [CabecalhoTexto1_Index], [Texto1_Index], [Logo], [Video_Index], [NotaDeCorte], [CabecalhoTexto2_Index], [Texto2_Index], [CabecalhoTexto3_Index], [Texto3_Index], [TextoAlvo_Index], [TextoGrafico_Index], [TextoComputador_Index], [EnderecoLinha1], [EnderecoLinha2], [EnderecoLinha3], [EmailContato], [LogoBackground]) VALUES (N'1', N'ESF', N'Suas finanças de maneira inteligente', N'O objetivo desta plataforma é conectar suas FINANÇAS aos seus sonhos, através de nossos métodos você irá trilhar o caminho do conhecimento rumo ao seu objetivo de vida. Vem conosco !!!', N'/assets/logos/Logo_escuro_2a11.jpg', N'/uploads/Institucional_Oficial_v.1.mp4', N'5.0', N'CONHEÇA NOSSOS CURSOS', N'Sucesso financeiro é ter dinheiro suficiente para fazer aquilo que você deseja, de forma planejada. Nossos cursos te dará a direção, e proporcionarão ferramentas para que você se torne uma pessoa financeiramente bem-sucedida, ter dinheiro suficiente para cobrir o seu custo de vida e realizar projetos futuros, sem precisar se endividar, por exemplo.', N'COMO FUNCIONA', N'Você aprenderá a prosperar financeiramente através de 5 bases simples: 1. Mudança de mentalidade financeira; 2. Poupar; 3. Investir; 4. Renda Extra; e 5. Simplificação. Você descobrirá oportunidades incríveis para aumentar as receitas, controlar as despesas, investir em ativos de alta rentabilidade, viver um estilo de vida simples e abundante! Quando você enriquece o mundo se torna um lugar melhor!', N'A meta é a vida abundante! O que você não tem é pelo que você ainda não conhece. Você possui uma mente infinitamente criativa, uma capacidade infinita de ser uma pessoa rica. Só precisa aprender a usá-la.', N'A parte mais trabalhosa de enriquecer é tomar deliberadamente a decisão de começar. Todas as demais etapas são bem mais simples. O que você precisa fazer agora é decidir se tornar uma pessoa próspera financeiramente e começar a agir!', N'Pesquisas demonstram que 95% das pessoas se aponsentam com renda insuficiente na velhice, e dependem do governo ou de parentes. Milhões de brasileiros vivem essa realidade. Você pode planejar agora as finanças dos próximos anos e virar essa equação em seu favor. Te pegaremos pela mão e te auxiliaremos a planejar a sua reserva financeira para hoje e para o futuro.', N'Av. Dr. Nelson d''''Ávila, 1837', N'Centro - Sao Jose dos Campos', null, N'contato@upfirst.com.br', N'RGB(6,26,55)');
GO
SET IDENTITY_INSERT [Configuracoes] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Cursos]
-- ----------------------------
DROP TABLE [Cursos]
GO
CREATE TABLE [Cursos] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Nome] nvarchar(MAX) NULL ,
[Descricao] nvarchar(MAX) NULL ,
[Preco] decimal(6,2) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[Cursos]', RESEED, 3)
GO

-- ----------------------------
-- Records of Cursos
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Cursos] ON
GO
INSERT INTO [Cursos] ([Id], [Nome], [Descricao], [Preco]) VALUES (N'2', N'EDUCAÇÃO FINANCEIRA', null, N'399.00');
GO
SET IDENTITY_INSERT [Cursos] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [MercadoPago_WebHooks]
-- ----------------------------
DROP TABLE [MercadoPago_WebHooks]
GO
CREATE TABLE [MercadoPago_WebHooks] (
[MercadoPago_WebHookId] int NOT NULL IDENTITY(1,1) ,
[Data] datetime2(7) NOT NULL ,
[DataId] bigint NOT NULL ,
[Type] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[MercadoPago_WebHooks]', RESEED, 18)
GO

-- ----------------------------
-- Records of MercadoPago_WebHooks
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [MercadoPago_WebHooks] ON
GO
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'1', N'2021-01-25 07:38:04.9767628', N'123', N'test');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'2', N'2021-01-25 07:45:50.4654285', N'1233084404', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'3', N'2021-01-25 08:45:04.6905000', N'1233083868', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'4', N'2021-01-28 21:45:46.2152017', N'1233047719', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'5', N'2021-01-28 22:15:33.7622486', N'1233047854', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'6', N'2021-01-29 12:00:18.7539809', N'1233325070', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'7', N'2021-01-29 12:03:54.3710471', N'1233325157', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'8', N'2021-01-29 12:18:40.3955743', N'1233325285', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'9', N'2021-02-01 21:50:39.7050900', N'1233047719', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'10', N'2021-02-01 22:20:57.5387577', N'1233047854', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'11', N'2021-02-02 09:28:05.8449772', N'1233416522', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'12', N'2021-02-02 09:39:56.5342530', N'1233415621', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'13', N'2021-02-02 12:31:54.9001165', N'1233421058', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'14', N'2021-02-03 08:20:41.7580711', N'1233451600', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'15', N'2021-02-03 11:51:36.1877108', N'1233458461', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'16', N'2021-02-03 11:51:37.1709364', N'1233458461', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'17', N'2021-02-03 12:10:13.6138543', N'1233458461', N'payment');
INSERT INTO [MercadoPago_WebHooks] ([MercadoPago_WebHookId], [Data], [DataId], [Type]) VALUES (N'18', N'2021-02-03 12:46:03.2178107', N'1233458461', N'payment');
GO
SET IDENTITY_INSERT [MercadoPago_WebHooks] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Modulos]
-- ----------------------------
DROP TABLE [Modulos]
GO
CREATE TABLE [Modulos] (
[Id] int NOT NULL IDENTITY(1,1) ,
[CursoId] int NOT NULL ,
[AvaliacaoId] int NULL ,
[Descricao] nvarchar(MAX) NULL ,
[NumeroModulo] int NOT NULL 
)


GO
DBCC CHECKIDENT(N'[Modulos]', RESEED, 7)
GO

-- ----------------------------
-- Records of Modulos
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Modulos] ON
GO
INSERT INTO [Modulos] ([Id], [CursoId], [AvaliacaoId], [Descricao], [NumeroModulo]) VALUES (N'3', N'2', N'2', N'Modulo 1', N'1');
INSERT INTO [Modulos] ([Id], [CursoId], [AvaliacaoId], [Descricao], [NumeroModulo]) VALUES (N'4', N'2', N'3', N'Modulo 2', N'2');
INSERT INTO [Modulos] ([Id], [CursoId], [AvaliacaoId], [Descricao], [NumeroModulo]) VALUES (N'5', N'2', N'4', N'Modulo 3', N'3');
INSERT INTO [Modulos] ([Id], [CursoId], [AvaliacaoId], [Descricao], [NumeroModulo]) VALUES (N'6', N'2', N'5', N'Modulo 4', N'4');
INSERT INTO [Modulos] ([Id], [CursoId], [AvaliacaoId], [Descricao], [NumeroModulo]) VALUES (N'7', N'2', N'6', N'Modulo 5', N'5');
GO
SET IDENTITY_INSERT [Modulos] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Pagamentos]
-- ----------------------------
DROP TABLE [Pagamentos]
GO
CREATE TABLE [Pagamentos] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Valor] decimal(5,2) NOT NULL ,
[Data] datetime2(7) NOT NULL ,
[Forma] nvarchar(MAX) NULL ,
[OrderId] nvarchar(MAX) NULL ,
[PaymentId] bigint NOT NULL ,
[TipoPagamento] nvarchar(MAX) NULL ,
[StatusDetail] nvarchar(MAX) NULL ,
[Status] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[Pagamentos]', RESEED, 17)
GO

-- ----------------------------
-- Records of Pagamentos
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Pagamentos] ON
GO
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'1', N'399.00', N'2021-01-25 07:45:50.8439874', N'Mercado Pago', N'2254378633', N'1233084404', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'2', N'399.00', N'2021-01-25 08:45:04.9752871', N'Mercado Pago', N'2254633383', N'1233083868', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'6', N'399.00', N'2021-01-29 12:03:54.5821217', N'Mercado Pago', N'2269428991', N'1233325157', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'7', N'399.00', N'2021-01-29 12:18:40.6119823', N'Mercado Pago', N'2269489084', N'1233325285', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'8', N'399.00', N'2021-02-01 21:50:40.9247324', N'Mercado Pago', N'2247111314', N'1233047719', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'9', N'399.00', N'2021-02-01 22:20:57.7461157', N'Mercado Pago', N'2247246453', N'1233047854', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'10', N'399.00', N'2021-02-02 09:28:06.2009110', N'Mercado Pago', N'2282715656', N'1233416522', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'11', N'399.00', N'2021-02-02 09:39:56.7378471', N'Mercado Pago', N'2282763906', N'1233415621', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'12', N'399.00', N'2021-02-02 12:31:55.3426926', N'Mercado Pago', N'2283429692', N'1233421058', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'14', N'5.10', N'2021-02-03 11:51:36.6603478', N'Mercado Pago', N'2287219606', N'1233458461', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'15', N'5.10', N'2021-02-03 11:51:37.3606246', N'Mercado Pago', N'2287219606', N'1233458461', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'16', N'5.10', N'2021-02-03 12:10:13.8404189', N'Mercado Pago', N'2287219606', N'1233458461', N'credit_card', N'accredited', N'approved');
INSERT INTO [Pagamentos] ([Id], [Valor], [Data], [Forma], [OrderId], [PaymentId], [TipoPagamento], [StatusDetail], [Status]) VALUES (N'17', N'5.10', N'2021-02-03 12:46:03.4338236', N'Mercado Pago', N'2287219606', N'1233458461', N'credit_card', N'accredited', N'approved');
GO
SET IDENTITY_INSERT [Pagamentos] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [PerguntasAvaliacao]
-- ----------------------------
DROP TABLE [PerguntasAvaliacao]
GO
CREATE TABLE [PerguntasAvaliacao] (
[Id] int NOT NULL IDENTITY(1,1) ,
[AvaliacaoId] int NOT NULL ,
[Descricao] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[PerguntasAvaliacao]', RESEED, 13)
GO

-- ----------------------------
-- Records of PerguntasAvaliacao
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [PerguntasAvaliacao] ON
GO
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'1', N'1', N'Quanto é 1 x 1 ?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'2', N'1', N'Quanto é 2 x 6 ?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'3', N'2', N'Qual alternativa não faz parte das 5 bases deste curso?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'4', N'3', N'Marque a única alternativa incorreta. As 6 leis da autorresponsabilidade financeira são?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'5', N'4', N'Marque as 3 alternativas corretas. Riqueza verdadeira é aquela que combina as três dimensões humanas, quais são elas?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'6', N'5', N'Na aula sobre construção de RENDA EXTRA, ensinamos que para se tornar um especialista em sua área de atuação é preciso concluir um macrociclo de 1.000 horas. O macrociclo é formado por 22 semanas de microciclos aplicados. A pergunta é: um microciclo de 45 horas semanais é composto por?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'7', N'6', N'Marque a única alternativa correta. Na aula sobre SIMPLIFICAÇÃO, ensinamos o seguinte conceito:');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'8', N'2', N'Qual alternativa não faz parte dos 11 pilares do MAF – Mapa de Autoavaliação Financeira?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'10', N'3', N'De acordo com a aula 3 deste módulo 2, o segredo dos vencedores é?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'11', N'4', N'De quem é a célebre frase mencionada na aula 1 deste módulo 3: “Para que as coisas mudem, você tem que mudar.... Para que as coisas melhorem, você tem que melhorar.... Podemos ter mais do que já temos, porque podemos nos tornar melhores do que somos.”.');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'12', N'5', N'A média de ganho de um especialista é?');
INSERT INTO [PerguntasAvaliacao] ([Id], [AvaliacaoId], [Descricao]) VALUES (N'13', N'6', N'A quantidade de salários sugeridos que você ganha, para compor a sua reserva financeira é?');
GO
SET IDENTITY_INSERT [PerguntasAvaliacao] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [PerguntasQuestionario]
-- ----------------------------
DROP TABLE [PerguntasQuestionario]
GO
CREATE TABLE [PerguntasQuestionario] (
[Id] int NOT NULL IDENTITY(1,1) ,
[QuestionarioId] int NOT NULL ,
[Descricao] nvarchar(MAX) NULL ,
[Resposta] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[PerguntasQuestionario]', RESEED, 9)
GO

-- ----------------------------
-- Records of PerguntasQuestionario
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [PerguntasQuestionario] ON
GO
INSERT INTO [PerguntasQuestionario] ([Id], [QuestionarioId], [Descricao], [Resposta]) VALUES (N'7', N'1', N'Qual seu conhecimento em despesas e receitas?', null);
INSERT INTO [PerguntasQuestionario] ([Id], [QuestionarioId], [Descricao], [Resposta]) VALUES (N'8', N'1', N'Qual seu conhecimento em investimentos?', null);
INSERT INTO [PerguntasQuestionario] ([Id], [QuestionarioId], [Descricao], [Resposta]) VALUES (N'9', N'1', N'Qual o seu conhecimento em controle financeiro?', null);
GO
SET IDENTITY_INSERT [PerguntasQuestionario] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [Questionarios]
-- ----------------------------
DROP TABLE [Questionarios]
GO
CREATE TABLE [Questionarios] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Descricao] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of Questionarios
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [Questionarios] ON
GO
INSERT INTO [Questionarios] ([Id], [Descricao]) VALUES (N'1', N'Questionário Inicial');
GO
SET IDENTITY_INSERT [Questionarios] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for [RespostasAvaliacao]
-- ----------------------------
DROP TABLE [RespostasAvaliacao]
GO
CREATE TABLE [RespostasAvaliacao] (
[Id] int NOT NULL IDENTITY(1,1) ,
[PerguntaAvaliacaoId] int NOT NULL ,
[Descricao] nvarchar(MAX) NULL ,
[Correta] bit NOT NULL 
)


GO
DBCC CHECKIDENT(N'[RespostasAvaliacao]', RESEED, 60)
GO

-- ----------------------------
-- Records of RespostasAvaliacao
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [RespostasAvaliacao] ON
GO
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'1', N'1', N'Zero', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'2', N'1', N'Um', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'3', N'1', N'Dois', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'4', N'1', N'Três', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'5', N'2', N'6', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'6', N'2', N'13', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'7', N'2', N'12', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'8', N'2', N'14', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'9', N'3', N'Mudança de mentalidade financeira', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'10', N'4', N'Se for criticar as pessoas, cale-se', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'11', N'5', N'Dimensão do SER, caracterizado pela CRENÇA DE IDENTIDADE', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'12', N'6', N'7 horas de trabalho, e 2 horas de estudos diários', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'13', N'7', N'Aumente o seu padrão de vida à medida em que sua renda aumentar', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'14', N'3', N'Poupar', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'15', N'3', N'Investir', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'16', N'3', N'Renda extra', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'17', N'3', N'Simplificação', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'18', N'3', N'Gastar', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'19', N'4', N'Se for reclamar das circunstâncias, dê sugestão', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'20', N'4', N'Se for investir, invista na caderneta de poupança', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'21', N'4', N'Se for buscar culpados, busque a solução', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'22', N'4', N'Se for se fazer de vítima, faça-se de vencedor', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'23', N'4', N'Se for justificar os seus erros, aprenda com eles', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'24', N'4', N'Se for julgar alguém, julgue a atitude dessa pessoa', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'25', N'5', N'Dimensão do FAZER, caracterizado pela CRENÇA DE CAPACIDADE', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'26', N'5', N'Dimensão do TER, caracterizado pela CRENÇA DE MERECIMENTO', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'27', N'5', N'Dimensão da ECONOMIA, caracterizado pela CRENÇA DE POUPAR', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'28', N'5', N'Dimensão do TRABALHO, caracterizado pela CRENÇA DE PRODUZIR', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'29', N'6', N'6 horas de trabalho, e 3 horas de estudos diários', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'30', N'6', N'9 horas de trabalho, e 1 hora de estudos diários', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'31', N'6', N'8 horas de trabalho, e 1 hora de estudos diários', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'32', N'6', N'1 hora de trabalho, e 8 horas de estudos diários', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'33', N'7', N'Aumente a sua renda à medida em que o seu padrão de vida aumentar', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'34', N'7', N'Aumente o seu padrão de vida somente após alcançar o objetivo definido em seu plano financeiro', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'35', N'7', N'Simplifique a sua vida simplesmente aumentando o seu padrão de vida', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'36', N'7', N'Simplifique a sua vida simplesmente aumentando a sua renda', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'37', N'8', N'Pagar a si mesmo', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'38', N'8', N'Pagar as dívidas', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'39', N'8', N'Poupar para a sua segurança financeira', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'40', N'8', N'Investir', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'41', N'8', N' Seguros', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'42', N'10', N'Ter um bom rendimento mensal', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'43', N'10', N'Saber diferenciar dívidas de contas mensais', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'44', N'10', N'Ser diligente', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'45', N'10', N'Ser uma pessoa inteligente', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'46', N'10', N'Ter uma aplicação específica para os estudos dos filhos', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'47', N'10', N'Investir em ativos de alta rentabilidade', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'48', N'11', N'Wallace D. Wattles', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'49', N'11', N'Bob Proctor', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'50', N'11', N'Paulo Vieira', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'51', N'11', N'Jim Rohn', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'52', N'11', N'Paul Mackenna', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'53', N'12', N'Especialista nível I: Ganha 3 vezes mais que um profissional comum', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'54', N'12', N'Especialista nível II: Ganha 6 vezes mais que um profissional comum', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'55', N'12', N'Especialista nível III: Ganha 8 vezes mais que um profissional comum', N'1');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'56', N'12', N'Especialista nível IV: Ganha 12 vezes mais que um profissional comum', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'57', N'12', N'Especialista nível V: Ganha 15 vezes mais que um profissional comum', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'58', N'13', N'Para formação de sua PROTEÇÃO FINANCEIRA: 6 salários para empregados; 12 salários para autônomos ou empreendedores', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'59', N'13', N'Para formação de sua SEGURANÇA FINANCEIRA: 120 salários', N'0');
INSERT INTO [RespostasAvaliacao] ([Id], [PerguntaAvaliacaoId], [Descricao], [Correta]) VALUES (N'60', N'13', N'Para formação de sua LIBERDADE FINANCEIRA: 240 salários', N'1');
GO
SET IDENTITY_INSERT [RespostasAvaliacao] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Indexes structure for table Alunos
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Alunos]
-- ----------------------------
ALTER TABLE [Alunos] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table ArquivosApoio
-- ----------------------------
CREATE INDEX [IX_ArquivosApoio_AulaId] ON [ArquivosApoio]
([AulaId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [ArquivosApoio]
-- ----------------------------
ALTER TABLE [ArquivosApoio] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Aulas
-- ----------------------------
CREATE INDEX [IX_Aulas_ModuloId] ON [Aulas]
([ModuloId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [Aulas]
-- ----------------------------
ALTER TABLE [Aulas] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Avaliacoes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Avaliacoes]
-- ----------------------------
ALTER TABLE [Avaliacoes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Configuracoes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Configuracoes]
-- ----------------------------
ALTER TABLE [Configuracoes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Cursos
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Cursos]
-- ----------------------------
ALTER TABLE [Cursos] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table MercadoPago_WebHooks
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [MercadoPago_WebHooks]
-- ----------------------------
ALTER TABLE [MercadoPago_WebHooks] ADD PRIMARY KEY ([MercadoPago_WebHookId])
GO

-- ----------------------------
-- Indexes structure for table Modulos
-- ----------------------------
CREATE UNIQUE INDEX [IX_Modulos_AvaliacaoId] ON [Modulos]
([AvaliacaoId] ASC) 
WHERE ([AvaliacaoId] IS NOT NULL)
GO
CREATE INDEX [IX_Modulos_CursoId] ON [Modulos]
([CursoId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [Modulos]
-- ----------------------------
ALTER TABLE [Modulos] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Pagamentos
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Pagamentos]
-- ----------------------------
ALTER TABLE [Pagamentos] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table PerguntasAvaliacao
-- ----------------------------
CREATE INDEX [IX_PerguntasAvaliacao_AvaliacaoId] ON [PerguntasAvaliacao]
([AvaliacaoId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [PerguntasAvaliacao]
-- ----------------------------
ALTER TABLE [PerguntasAvaliacao] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table PerguntasQuestionario
-- ----------------------------
CREATE INDEX [IX_PerguntasQuestionario_QuestionarioId] ON [PerguntasQuestionario]
([QuestionarioId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [PerguntasQuestionario]
-- ----------------------------
ALTER TABLE [PerguntasQuestionario] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Questionarios
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [Questionarios]
-- ----------------------------
ALTER TABLE [Questionarios] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table RespostasAvaliacao
-- ----------------------------
CREATE INDEX [IX_RespostasAvaliacao_PerguntaAvaliacaoId] ON [RespostasAvaliacao]
([PerguntaAvaliacaoId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [RespostasAvaliacao]
-- ----------------------------
ALTER TABLE [RespostasAvaliacao] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Foreign Key structure for table [ArquivosApoio]
-- ----------------------------
ALTER TABLE [ArquivosApoio] ADD FOREIGN KEY ([AulaId]) REFERENCES [Aulas] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [Aulas]
-- ----------------------------
ALTER TABLE [Aulas] ADD FOREIGN KEY ([ModuloId]) REFERENCES [Modulos] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [Modulos]
-- ----------------------------
ALTER TABLE [Modulos] ADD FOREIGN KEY ([AvaliacaoId]) REFERENCES [Avaliacoes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [Modulos] ADD FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [PerguntasAvaliacao]
-- ----------------------------
ALTER TABLE [PerguntasAvaliacao] ADD FOREIGN KEY ([AvaliacaoId]) REFERENCES [Avaliacoes] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [PerguntasQuestionario]
-- ----------------------------
ALTER TABLE [PerguntasQuestionario] ADD FOREIGN KEY ([QuestionarioId]) REFERENCES [Questionarios] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [RespostasAvaliacao]
-- ----------------------------
ALTER TABLE [RespostasAvaliacao] ADD FOREIGN KEY ([PerguntaAvaliacaoId]) REFERENCES [PerguntasAvaliacao] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
