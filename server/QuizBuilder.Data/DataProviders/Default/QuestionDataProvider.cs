﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class QuestionDataProvider : IQuestionDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public QuestionDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<IEnumerable<QuestionDto>> GetByQuiz( string uid ) {

			const string sql = @"
			SELECT
				q.Id,
				q.UId,
				q.TypeId,
				q.Name,
				qi.SortOrder,
				q.Text,
				q.Points,
				q.Settings,
				pqi.UId AS ParentUId
			FROM 
				dbo.Question q WITH(NOLOCK)
			INNER JOIN
				dbo.QuizItem qi WITH(NOLOCK) ON q.Id = qi.QuestionId
			INNER JOIN
				dbo.QuizQuizItem qqi WITH(NOLOCK) ON qi.Id = qqi.QuizItemId
			INNER JOIN
				dbo.Quiz qz WITH(NOLOCK) ON qz.Id = qqi.QuizId
			INNER JOIN 
				dbo.QuizItem pqi WITH(NOLOCK) ON qi.ParentId = pqi.Id";

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<QuestionDto>( sql, new { QuizUId = uid } );
		}

		public async Task<IEnumerable<QuestionDto>> GetByGroup( string uid ) {
			const string sql = @"
			SELECT
				q.Id,
				q.UId,
				q.TypeId,
				q.Name,
				qi.SortOrder,
				q.Text,
				q.Points,
				q.Settings
			FROM dbo.Question AS q WITH(NOLOCK)
			INNER JOIN dbo.QuizItem AS qi WITH(NOLOCK)
				ON q.Id = qi.QuestionId
			WHERE qi.ParentId = (SELECT TOP 1 Id FROM dbo.QuizItem WITH(NOLOCK) WHERE UId = @GroupUId)";

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<QuestionDto>( sql, new { GroupUId = uid } );
		}

		public async Task<QuestionDto> Get( string uid ) {
			const string sql = @"
			SELECT
				Id,
				UId,
				TypeId,
				Name,
				qi.SortOrder,
				Text,
				Points,
				Settings
			FROM dbo.Question AS q (NOLOCK)
			INNER JOIN dbo.QuizItem AS qi WITH(NOLOCK)
				ON q.Id = qi.QuestionId
			WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuestionDto>( sql, new { UId = uid } );
		}

		public async Task<(long, long)> Add( long groupId, QuestionDto dto ) {

			const string sql = @"
		DECLARE @ID TABLE (ID INT)

		INSERT INTO dbo.Question(
			UId,
		    TypeId,
		    Name,
		    Text,
			Points,
		    Settings,
		    CreatedOn,
		    ModifiedOn
		)
		OUTPUT INSERTED.Id INTO @ID
		VALUES (
			@UId,
		    @TypeId,
		    @Name,
		    @Text,
			@Points,
		    @Settings,
		    @CreatedOn,
		    @ModifiedOn
		)

		INSERT INTO dbo.QuizItem (
			UId,
		    TypeId,
		    ParentId,
		    QuestionId,
			SortOrder,
			Name,
		    CreatedOn,
		    ModifiedOn
		)
		OUTPUT INSERTED.Id INTO @ID
		VALUES(
			@UId,
		    1,
		    @ParentId,
		    (SELECT TOP 1 ID FROM @ID),
			1 + (
 SELECT ISNULL(MAX(qi.SortOrder), 0)  FROM dbo.Question AS q
 INNER JOIN dbo.QuizItem AS qi ON qi.QuestionId = q.Id
 WHERE
	qi.TypeId = 1 AND
	qi.ParentId = @ParentId),
			'',
		    @CreatedOn,
		    @ModifiedOn
		)

		SELECT TOP 2 ID FROM @ID";

			using IDbConnection conn = GetConnection();
			var result  = (await conn.QueryAsync<long>( sql, new {
				dto.UId,
				dto.TypeId,
				dto.Name,
				dto.Text,
				dto.Points,
				dto.Settings,
				ParentId = groupId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} )).ToList();

			return (result[0], result[1]);

		}

		public async Task Update( QuestionDto dto ) {

			const string sql = @"
			UPDATE dbo.Question
			SET TypeId = @TypeId,
				Name = @Name,
				Text = @Text,
				Points = @Points,
				Settings = @Settings,
				ModifiedOn = @ModifiedOn
			WHERE Id = @Id

			UPDATE dbo.QuizItem
			SET SortOrder = @SortOrder
			WHERE QuestionId = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				dto.Id,
				dto.Name,
				dto.Text,
				dto.Points,
				dto.Settings,
				dto.TypeId,
				dto.SortOrder,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Delete( string uid ) {

			const string sql = @"
DELETE FROM dbo.QuizItem WHERE UId=@UId
DELETE FROM dbo.Question WHERE UId=@UId";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { UId = uid } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}
}
