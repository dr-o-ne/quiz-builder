using System;
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
				q.SortOrder,
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

		public async Task<IEnumerable<QuestionDto>> GetByParent( string quizUid, string groupUid ) {
			string groupFilter = string.IsNullOrWhiteSpace( groupUid )
				? " IS NULL"
				: " = (SELECT TOP 1 Id FROM dbo.QuizItem WITH(NOLOCK) WHERE UId = @GroupUId)"; // ToDo: update to normal state

			string sql = @"
			SELECT
				q.Id,
				q.UId,
				q.TypeId,
				q.Name,
				q.SortOrder,
				q.Text,
				q.Points,
				q.Settings
			FROM
				dbo.Question q WITH(NOLOCK)
			INNER JOIN
				dbo.QuizItem qi WITH(NOLOCK) ON qi.QuestionId = q.Id
			INNER JOIN
				dbo.QuizQuizItem qqi WITH(NOLOCK) ON qqi.QuizItemId = qi.Id
			INNER JOIN
				dbo.Quiz qz WITH(NOLOCK) ON qqi.QuizId = qz.Id
			WHERE
				qz.UId = @QuizUid AND qi.ParentId" + groupFilter;

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<QuestionDto>( sql, new { QuizUid = quizUid, GroupUId = groupUid } );
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
	Points,
	Settings
FROM dbo.Question ( NOLOCK )
WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuestionDto>( sql, new { UId = uid } );
		}

		public async Task<(long, long)> Add( QuestionDto dto ) {

			const string sql = @"
		DECLARE @ID TABLE (ID INT)

		INSERT INTO dbo.Question(
			UId,
		    TypeId,
			SortOrder,
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
			0, --TODO
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
			Name,
		    CreatedOn,
		    ModifiedOn
		)
		OUTPUT INSERTED.Id INTO @ID
		VALUES(
			@UId,
		    1,
		    NULL,
		    (SELECT TOP 1 ID FROM @ID),
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
	SortOrder = @SortOrder,
	Text = @Text,
	Points = @Points,
	Settings = @Settings,
	ModifiedOn = @ModifiedOn
WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				dto.Id,
				dto.Name,
				dto.SortOrder,
				dto.Text,
				dto.Points,
				dto.Settings,
				dto.TypeId,
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
