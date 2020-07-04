CREATE TABLE dbo.Quiz(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[UId] NVARCHAR(10) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[IsEnabled] BIT NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_QUIZ] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]

) ON [PRIMARY]
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT [DF_Quiz_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT [DF_Quiz_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT [DF_Quiz_IsEnabled]  DEFAULT ((0)) FOR [IsEnabled]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_Quiz_UId] ON dbo.Quiz ( [UId] ASC ) ON [PRIMARY]
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
    ('Group'),
    ('FreeText'),
    ('Pool')
GO

CREATE TABLE dbo.Question(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[UId] NVARCHAR(10) NOT NULL,
	[SortOrder] INT NOT NULL,
	[TypeId] BIGINT NOT NULL,
	[Name] NVARCHAR(255) NULL,
	[Text] NVARCHAR(MAX) NOT NULL,
	[Points] [decimal](18, 0) NULL,
	[Settings] NVARCHAR(MAX) NOT NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE dbo.Question ADD CONSTRAINT [DF_Question_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE dbo.Question ADD CONSTRAINT [DF_Question_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_Question_UId] ON dbo.Question ( [UId] ASC ) ON [PRIMARY]
GO

CREATE TABLE dbo.QuizItem(
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[UId] NVARCHAR(10) NOT NULL,
	[SortOrder] INT NULL,
	[TypeId] BIGINT NOT NULL,
	[ParentId] BIGINT NULL,
	[QuestionId] BIGINT NULL,
	[Name] NVARCHAR(255) NULL,
	[CreatedOn] DATETIME2(7) NOT NULL,
	[ModifiedOn] DATETIME2(7) NOT NULL,

	CONSTRAINT [PK_QuizItem] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT [DF_QuizItem_CreatedOn] DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT [DF_QuizItem_ModifiedOn] DEFAULT (getutcdate()) FOR [ModifiedOn]
GO

ALTER TABLE dbo.QuizItem WITH CHECK ADD CONSTRAINT [FK_QuizItem_QuizItemType] FOREIGN KEY([TypeId])
REFERENCES dbo.QuizItemType ([Id])
GO

ALTER TABLE dbo.QuizItem CHECK CONSTRAINT [FK_QuizItem_QuizItemType]
GO

ALTER TABLE dbo.QuizItem WITH CHECK ADD CONSTRAINT [FK_QuizItem_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
GO

ALTER TABLE [dbo].[QuizItem] CHECK CONSTRAINT [FK_QuizItem_Question]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_QuizItem_UId] ON dbo.QuizItem ( [UId] ASC ) ON [PRIMARY]
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

CREATE TABLE dbo.Attempt
(
  Id         BIGINT IDENTITY(1, 1) NOT NULL, 
  UId        NVARCHAR(10) NOT NULL, 
  QuizId     BIGINT NOT NULL, 
  StartDate  DATETIME2(7) NOT NULL, 
  EndDate    DATETIME2(7) NULL, 
  Data       NVARCHAR(MAX) NULL,
  Result     DECIMAL(18, 0) NULL, 
  CreatedOn  DATETIME2(7) NOT NULL, 
  ModifiedOn DATETIME2(7) NOT NULL, 
  CONSTRAINT PK_Attempt PRIMARY KEY CLUSTERED(Id ASC) ON [PRIMARY])
ON [PRIMARY];
GO

ALTER TABLE dbo.Attempt
ADD CONSTRAINT DF_Attempt_CreatedOn DEFAULT GETUTCDATE() FOR CreatedOn;
GO

ALTER TABLE dbo.Attempt
ADD CONSTRAINT DF_Attempt_ModifiedOn DEFAULT GETUTCDATE() FOR ModifiedOn;
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_Attempt_UId] ON dbo.Attempt ( [UId] ASC ) ON [PRIMARY]
GO