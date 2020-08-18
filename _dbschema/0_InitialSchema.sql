CREATE TABLE dbo.Organization(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	UId NVARCHAR(10) NOT NULL,
	CreatedOn DATETIME2(7) NOT NULL,
	ModifiedOn DATETIME2(7) NOT NULL,

	CONSTRAINT PK_ORGANIZATION PRIMARY KEY CLUSTERED ( Id ASC )
)
GO

ALTER TABLE dbo.Organization ADD CONSTRAINT DF_Organization_CreatedOn DEFAULT (getutcdate()) FOR CreatedOn
GO

ALTER TABLE dbo.Organization ADD CONSTRAINT DF_Organization_ModifiedOn DEFAULT (getutcdate()) FOR ModifiedOn
GO

CREATE TABLE dbo.Quiz(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	UId NVARCHAR(10) NOT NULL,
	OrgId BIGINT NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	IsEnabled BIT NOT NULL,
	Settings NVARCHAR(MAX) NOT NULL,
	CreatedOn DATETIME2(7) NOT NULL,
	ModifiedOn DATETIME2(7) NOT NULL,

	CONSTRAINT PK_QUIZ PRIMARY KEY CLUSTERED ( Id ASC )
)
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT DF_Quiz_CreatedOn DEFAULT (getutcdate()) FOR CreatedOn
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT DF_Quiz_ModifiedOn DEFAULT (getutcdate()) FOR ModifiedOn
GO

ALTER TABLE dbo.Quiz ADD CONSTRAINT DF_Quiz_IsEnabled  DEFAULT ((0)) FOR IsEnabled
GO

CREATE UNIQUE NONCLUSTERED INDEX UI_Quiz_UId ON dbo.Quiz ( UId ASC )
GO


CREATE TABLE dbo.QuizItemType(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(252) NOT NULL,

	CONSTRAINT PK_QUIZITEMTYPE PRIMARY KEY CLUSTERED ( Id ASC )

)
GO

INSERT INTO dbo.QuizItemType (Name)
VALUES
    ('Group'),
    ('TrueFalse'),
    ('MultiChoice'),
    ('FillInTheBlanks'),
    ('MultiSelect'),
    ('LongAnswer')
GO

CREATE TABLE dbo.QuizItem(
	Id BIGINT IDENTITY(1,1) NOT NULL,
	UId NVARCHAR(10) NOT NULL,
	ParentId BIGINT NULL,
	TypeId BIGINT NOT NULL,
	Name NVARCHAR(255) NULL,
	Text NVARCHAR(MAX) NULL,
	IsEnabled BIT NULL,
	SortOrder INT NULL,
	Settings NVARCHAR(MAX) NULL,
	CreatedOn DATETIME2(7) NOT NULL,
	ModifiedOn DATETIME2(7) NOT NULL,

	CONSTRAINT PK_QuizItem PRIMARY KEY CLUSTERED ( Id ASC )
)
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT DF_QuizItem_CreatedOn DEFAULT (getutcdate()) FOR CreatedOn
GO

ALTER TABLE dbo.QuizItem ADD  CONSTRAINT DF_QuizItem_ModifiedOn DEFAULT (getutcdate()) FOR ModifiedOn
GO

ALTER TABLE dbo.QuizItem WITH CHECK ADD CONSTRAINT FK_QuizItem_QuizItemType FOREIGN KEY(TypeId)
REFERENCES dbo.QuizItemType (Id)
GO

ALTER TABLE dbo.QuizItem CHECK CONSTRAINT FK_QuizItem_QuizItemType
GO

CREATE UNIQUE NONCLUSTERED INDEX UI_QuizItem_UId ON dbo.QuizItem ( UId ASC )
GO

CREATE TABLE dbo.QuizQuizItem(
	Id 		   	BIGINT IDENTITY(1,1) NOT NULL,
	QuizId 		BIGINT NOT NULL,
	QuizItemId 	BIGINT NOT NULL,
	CreatedOn 	DATETIME2(7) NOT NULL,
	ModifiedOn 	DATETIME2(7) NOT NULL,

	CONSTRAINT PK_QUIZQUIZITEM PRIMARY KEY CLUSTERED ( Id ASC )
)
GO

ALTER TABLE dbo.QuizQuizItem ADD  CONSTRAINT DF_QuizQuizItem_CreatedOn DEFAULT (getutcdate()) FOR CreatedOn
GO

ALTER TABLE dbo.QuizQuizItem ADD  CONSTRAINT DF_QuizQuizItem_ModifiedOn DEFAULT (getutcdate()) FOR ModifiedOn
GO

ALTER TABLE dbo.QuizQuizItem WITH CHECK ADD CONSTRAINT FK_QuizQuizItem_Quiz FOREIGN KEY( QuizId )
REFERENCES dbo.Quiz ( Id )
GO

ALTER TABLE dbo.QuizQuizItem CHECK CONSTRAINT FK_QuizQuizItem_Quiz
GO

ALTER TABLE dbo.QuizQuizItem WITH CHECK ADD  CONSTRAINT FK_QuizQuizItem_QuizItem FOREIGN KEY( QuizItemId )
REFERENCES dbo.QuizItem ( Id )
GO

ALTER TABLE dbo.QuizQuizItem CHECK CONSTRAINT FK_QuizQuizItem_QuizItem
GO

CREATE TABLE dbo.Attempt(
	Id 			BIGINT IDENTITY(1, 1) NOT NULL, 
	UId			NVARCHAR(10) NOT NULL, 
	QuizId		BIGINT NOT NULL, 
	StartDate	DATETIME2(7) NOT NULL, 
	EndDate		DATETIME2(7) NULL, 
	Data		NVARCHAR(MAX) NULL,
	Result		DECIMAL(18, 0) NULL, 
	CreatedOn	DATETIME2(7) NOT NULL, 
	ModifiedOn	DATETIME2(7) NOT NULL, 

	CONSTRAINT PK_Attempt PRIMARY KEY CLUSTERED( Id ASC )
)
GO

ALTER TABLE dbo.Attempt
ADD CONSTRAINT DF_Attempt_CreatedOn DEFAULT GETUTCDATE() FOR CreatedOn;
GO

ALTER TABLE dbo.Attempt
ADD CONSTRAINT DF_Attempt_ModifiedOn DEFAULT GETUTCDATE() FOR ModifiedOn;
GO

CREATE UNIQUE NONCLUSTERED INDEX UI_Attempt_UId ON dbo.Attempt ( UId ASC )
GO