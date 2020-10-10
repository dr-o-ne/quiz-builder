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
SELECT a.Id,
       a.UId,
       q.UId AS QuizUId,
       a.StartDate,
       a.EndDate,
       a.[Data],
       a.TotalScore
FROM dbo.Attempt AS a (NOLOCK)
INNER JOIN dbo.Quiz AS q (NOLOCK) ON a.QuizId = q.Id
WHERE a.UId = @UId";

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
    TotalScore = @TotalScore,
	ModifiedOn = @ModifiedOn
WHERE UId = @UId";
			
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				OrgId = orgId,
				UserId = userId,
				dto.UId,
				dto.EndDate,
				dto.TotalScore,
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
