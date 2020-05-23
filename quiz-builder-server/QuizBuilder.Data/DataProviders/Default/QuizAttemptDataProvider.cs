using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class QuizAttemptDataProvider : IQuizAttemptDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public QuizAttemptDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<long> Add( AttemptDto dto ) {
			const string sql = @"
INSERT INTO dbo.Attempt (
	UId,
	QuizId,
	StartDate,
	CreatedOn,
	ModifiedOn)
OUTPUT INSERTED.Id
VALUES (
	@UId,
	(SELECT TOP 1 Id FROM dbo.Quiz WHERE UId = @QuizUId),
	@StartDate,
	@CreatedOn,
	@ModifiedOn
)";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteScalarAsync<long>( sql, new {
				UId = dto.UId,
				QuizUId = dto.QuizUId,
				StartDate = dto.StartDate,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}

	}
}
