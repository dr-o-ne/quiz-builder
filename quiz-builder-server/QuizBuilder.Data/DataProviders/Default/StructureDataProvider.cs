using System;
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

		public async Task AddQuizGroupRelationship( long quizId, long groupId ) {
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
				QuizItemId = groupId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task AddGroupQuestionRelationship( long groupId, long questionId ) {
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

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
