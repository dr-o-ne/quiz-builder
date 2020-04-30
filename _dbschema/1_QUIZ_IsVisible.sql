ALTER TABLE dbo.Quiz
ADD [IsVisible] BIT CONSTRAINT DF_Quiz_IsVisible DEFAULT ((0))
GO