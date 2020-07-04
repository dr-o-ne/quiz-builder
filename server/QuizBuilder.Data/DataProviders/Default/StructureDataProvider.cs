using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class StructureDataProvider : IStructureDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public StructureDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task AddQuizQuestionRelationship( long quizId, long quizItemId ) {

			const string sql = @"
INSERT INTO dbo.QuizQuizItem (
	QuizId,
    QuizItemId,
    CreatedOn,
    ModifiedOn
)
VALUES (
	@QuizId,
    @QuizItemId,
    @CreatedOn,
    @ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				QuizId = quizId,
				QuizItemId = quizItemId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task AddGroupQuestionRelationship( long? groupId, long questionId ) {
			const string sql = @"
		UPDATE
			dbo.QuizItem
		SET
			ParentId = @GroupId
		WHERE
			Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { GroupId = groupId, Id = questionId } );
		}

		public async Task DeleteQuizQuestionRelationship( string quizUId, string quizItemUId ) {
			const string sql = @"
DELETE qqi
FROM dbo.QuizQuizItem qqi
INNER JOIN dbo.Quiz q ON q.Id = qqi.QuizId
INNER JOIN dbo.QuizItem qi ON qi.Id = qqi.QuizItemId
WHERE
	q.UId = @QuizUId AND
	qi.UId = @QuizItemId";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { QuizUId = quizUId, QuizItemId = quizItemUId } );
		}

		public async Task<IEnumerable<(string, int)>> DeleteQuizRelationships( string quizUId ) {
			const string sql = @"
DECLARE @QuizQuizItemIds TABLE
( Id BIGINT )

DELETE qqi
OUTPUT DELETED.Id INTO @QuizQuizItemIds
FROM dbo.QuizQuizItem qqi
INNER JOIN dbo.Quiz q ON q.Id = qqi.QuizId
WHERE q.UId = @QuizUId

SELECT UId, q.TypeId
FROM dbo.QuizItem q
INNER JOIN @QuizQuizItemIds AS temp ON temp.Id = q.Id
";

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<(string, int)>( sql, new { QuizUId = quizUId } );
		}

		public async Task<int> RemoveQuizItemRelationships( string quizItemUId ) {
			const string sql = @"
			UPDATE
				dbo.QuizItem
			SET
				ParentId = NULL
			WHERE
				ParentId = (
					SELECT TOP 1
						Id
					FROM
						dbo.QuizItem WITH(NOLOCK)
					WHERE
						UId = @UId
				)";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteAsync( sql, new { UId = quizItemUId } );
		}

		public async Task<int> DeleteQuizQuizItemRelationships( string quizItemUId ) {
			const string sql = @"
			DELETE
				qqi
			FROM
				dbo.QuizQuizItem qqi WITH(NOLOCK)
			INNER JOIN
				dbo.QuizItem qi WITH(NOLOCK)
					ON qqi.QuizItemId = qi.Id
						AND qi.UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteAsync( sql, new { UId = quizItemUId } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}

		public async Task<long> GetQuizItemIdByQuestionUid( string uid ) {
			const string sql = @"
			SELECT TOP 1
				qi.Id
			FROM
				dbo.QuizItem qi WITH(NOLOCK)
			INNER JOIN
				dbo.Question q WITH(NOLOCK)
					ON qi.QuestionId = q.Id
			WHERE
				q.UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QueryFirstOrDefaultAsync<long>( sql, new { UId = uid } );
		}
	}

}
