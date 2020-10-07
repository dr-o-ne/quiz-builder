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

		public async Task<AttemptDto> Get( string uid ) {
			const string sql = @"
SELECT Id,
       UId,
       QuizId,
       StartDate,
       EndDate,
       [Data],
       Result
FROM dbo.Attempt (NOLOCK)
WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<AttemptDto>( sql, new { UId = uid } );
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

		public async Task Update( long orgId, string userId, AttemptDto dto ) {

			const string sql = @"
UPDATE dbo.Attempt
SET EndDate = @EndDate,
    Result = @Result,
	ModifiedOn = @ModifiedOn
WHERE UId = @UId";
			
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				OrgId = orgId,
				UserId = userId,
				dto.UId,
				dto.EndDate,
				dto.Result,
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
