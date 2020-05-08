CREATE TABLE dbo.Quiz(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[ClientId] NVARCHAR(10) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[IsVisible] BIT NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_QUIZ] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Quiz] ADD CONSTRAINT [DF_Quiz_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Quiz] ADD CONSTRAINT [DF_Quiz_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE [dbo].[Quiz] ADD CONSTRAINT [DF_Quiz_IsVisible]  DEFAULT ((0)) FOR [IsVisible]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_ClientId] ON [dbo].[Quiz] ( [ClientId] ASC ) ON [PRIMARY]
GO


CREATE TABLE dbo.QuizItemType(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(252) NOT NULL,

	CONSTRAINT [PK_QUIZITEMTYPE] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]

) ON [PRIMARY]
GO

INSERT INTO dbo.QuizItemType ([Name])
VALUES
    ('Question'),
    ('Section'),
    ('FreeText'),
    ('QuestionPool')
GO

CREATE TABLE dbo.Question(
	[Id] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL PRIMARY KEY CLUSTERED DEFAULT NEWSEQUENTIALID(),
	[QuestionTypeId] BIGINT NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[QuestionText] NVARCHAR(MAX) NOT NULL,
	[Settings] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Question] ADD CONSTRAINT [DF_Question_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Question] ADD CONSTRAINT [DF_Question_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

CREATE TABLE dbo.QuizItem(
	[Id] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL PRIMARY KEY CLUSTERED DEFAULT NEWSEQUENTIALID(),
	[QuizItemTypeId] BIGINT NOT NULL,
	[ParentQuizItemId] BIGINT NULL,
	[QuestionId] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL
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
	[QuizId] UNIQUEIDENTIFIER NOT NULL,
	[QuizItemId] UNIQUEIDENTIFIER NOT NULL,
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