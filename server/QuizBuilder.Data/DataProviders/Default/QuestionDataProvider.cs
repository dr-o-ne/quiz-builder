using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
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

		public async Task<ImmutableArray<QuestionDto>> GetByQuiz( string uid ) {

			const string sql = @"
			SELECT
				qi.Id,
				qi.UId,
				qi.TypeId,
				qi.Name,
				qi.Text,
				qi.SortOrder,
				qi.Settings,
				pqi.UId AS ParentUId
			FROM 
				dbo.QuizItem AS qi (NOLOCK)
			INNER JOIN
				dbo.QuizQuizItem AS qqi WITH(NOLOCK) ON qi.Id = qqi.QuizItemId
			INNER JOIN
				dbo.Quiz AS qz WITH(NOLOCK) ON qz.Id = qqi.QuizId
			INNER JOIN 
				dbo.QuizItem AS pqi WITH(NOLOCK) ON qi.ParentId = pqi.Id
			WHERE qz.UId = @QuizUId AND qi.TypeId <> 1";

			using IDbConnection conn = GetConnection();
			IEnumerable<QuestionDto> data = await conn.QueryAsync<QuestionDto>( sql, new { QuizUId = uid } );

			return data.ToImmutableArray();
		}

		public async Task<ImmutableArray<QuestionDto>> GetByGroup( string uid ) {
			const string sql = @"
			SELECT
				Id,
				UId,
				TypeId,
				Name,
				Text,
				SortOrder,
				Settings
			FROM dbo.QuizItem (NOLOCK)
			WHERE
				TypeId <> 1 AND
				ParentId = (SELECT TOP 1 Id FROM dbo.QuizItem (NOLOCK) WHERE UId = @GroupUId)";

			using IDbConnection conn = GetConnection();
			IEnumerable<QuestionDto> data = await conn.QueryAsync<QuestionDto>( sql, new { GroupUId = uid } );

			return data.ToImmutableArray();
		}

		public async Task<QuestionDto> Get( string uid ) {
			const string sql = @"
			SELECT
				Id,
				UId,
				TypeId,
				Name,
				SortOrder,
				Text,
				Settings
			FROM dbo.QuizItem (NOLOCK)
			WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuestionDto>( sql, new { UId = uid } );
		}

		public async Task<QuestionDto> Add( long groupId, QuestionDto dto ) {

			const string sql = @"

		INSERT INTO dbo.QuizItem (
			UId,
		    ParentId,
		    TypeId,
			Name,
		    Text,
			IsEnabled,
			SortOrder,
			Settings,
		    CreatedOn,
		    ModifiedOn
		)
		OUTPUT
			INSERTED.Id,
			INSERTED.UId,
			INSERTED.ParentId,
			INSERTED.TypeId,
			INSERTED.Name,
			INSERTED.Text,
			INSERTED.SortOrder,
			INSERTED.Settings,
			INSERTED.CreatedOn,
			INSERTED.ModifiedOn
		VALUES(
			@UId,
		    @ParentId,
		    @TypeId,
			@Name,
		    @Text,
			1,
			1 + (
				 SELECT ISNULL(MAX(SortOrder), 0)
				 FROM dbo.QuizItem
				 WHERE ParentId = @ParentId
			),
		    @Settings,
		    @CreatedOn,
		    @ModifiedOn
		)";

			using IDbConnection conn = GetConnection();
			return await conn.QueryFirstAsync<QuestionDto>( sql, new {
				dto.UId,
				dto.TypeId,
				dto.Name,
				dto.Text,
				dto.Settings,
				ParentId = groupId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Update( QuestionDto dto ) {

			const string sql = @"
			UPDATE dbo.QuizItem
			SET
				TypeId = @TypeId,
				SortOrder = @SortOrder,
				Name = @Name,
				Text = @Text,
				Settings = @Settings,
				ModifiedOn = @ModifiedOn
			WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				dto.Id,
				dto.Name,
				dto.Text,
				dto.Settings,
				dto.TypeId,
				dto.SortOrder,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Delete( string uid ) {

			const string sql = @"
			DELETE FROM dbo.QuizItem WHERE UId = @UId";

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
