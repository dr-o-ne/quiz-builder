USE master;
GO

IF DB_ID (N'QuizBuilder') IS NOT NULL
SET NOEXEC ON;
GO

CREATE DATABASE QuizBuilder;
GO

USE QuizBuilder;
GO

CREATE TABLE dbo.Quiz(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_QUIZ] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Quiz] ADD CONSTRAINT [DF_Quiz_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Quiz] ADD CONSTRAINT [DF_Quiz_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO


CREATE TABLE dbo.QuizItemType(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(252) NOT NULL,

	CONSTRAINT [PK_QUIZITEMTYPE] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]

) ON [PRIMARY]
GO


CREATE TABLE dbo.QuestionType(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_QUESTIONTYPE] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionT_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionT_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

CREATE TABLE dbo.Question(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[QuestionTypeId] BIGINT NOT NULL,
	[QuestionText] NVARCHAR(MAX) NOT NULL,
	[Settings] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_QUESTION] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Question] ADD CONSTRAINT [DF_Question_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Question] ADD CONSTRAINT [DF_Question_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE dbo.Question WITH CHECK ADD CONSTRAINT [Question_QuestionType] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionType] ([Id])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [Question_QuestionType]
GO

CREATE TABLE dbo.QuizItem(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[QuizItemTypeId] BIGINT NOT NULL,
	[ParentQuizItemId] BIGINT NULL,
	[QuestionId] BIGINT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_QUIZITEM] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT [DF_QuizItem_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT [DF_QuizItem_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE dbo.QuizItem WITH CHECK ADD CONSTRAINT [FK_QuizItem_QuizItemType] FOREIGN KEY([QuizItemTypeId])
REFERENCES dbo.QuizItemType ([Id])
GO

ALTER TABLE dbo.QuizItem CHECK CONSTRAINT [FK_QuizItem_QuizItemType]
GO

ALTER TABLE dbo.QuizItem WITH CHECK ADD CONSTRAINT [FK_QuizItem_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
GO

ALTER TABLE [dbo].[QuizItem] CHECK CONSTRAINT [FK_QuizItem_Question]
GO


CREATE TABLE dbo.QuizQuizItem(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[QuizId] BIGINT NOT NULL,
	[QuizItemId] BIGINT NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_QUIZQUIZITEM] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.QuizQuizItem ADD  CONSTRAINT [DF_QuizQuizItem_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE dbo.QuizQuizItem ADD  CONSTRAINT [DF_QuizQuizItem_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE dbo.QuizQuizItem WITH CHECK ADD CONSTRAINT [FK_QuizQuizItem_Quiz] FOREIGN KEY([QuizId])
REFERENCES dbo.Quiz ([Id])
GO

ALTER TABLE dbo.QuizQuizItem CHECK CONSTRAINT [FK_QuizQuizItem_Quiz]
GO

ALTER TABLE dbo.QuizQuizItem WITH CHECK ADD  CONSTRAINT [FK_QuizQuizItem_QuizItem] FOREIGN KEY([QuizItemId])
REFERENCES dbo.QuizItem ([Id])
GO

ALTER TABLE dbo.QuizQuizItem CHECK CONSTRAINT [FK_QuizQuizItem_QuizItem]
GO
