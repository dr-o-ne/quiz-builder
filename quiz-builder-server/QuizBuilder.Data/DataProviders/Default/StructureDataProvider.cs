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

		public Task AddQuizGroupRelationship( long quizid, long groupId ) {
			throw new System.NotImplementedException();
		}

		public Task AddGroupQuestionRelationship( long groupId, long questionId ) {
			throw new System.NotImplementedException();
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
