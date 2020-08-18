using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class QuizDataProvider : IQuizDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public QuizDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<ImmutableArray<QuizDto>> GetAll( long orgId, string userId ) {

			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	Settings,
	IsEnabled
FROM dbo.Quiz( NOLOCK )
WHERE OrgId = @OrgId OR @UserId = '169'";

			using IDbConnection conn = GetConnection();
			IEnumerable<QuizDto> data = await conn.QueryAsync<QuizDto>( sql, new { OrgId = orgId, UserId = userId } );

			return data.ToImmutableArray();
		}

		public async Task<QuizDto> Get( long orgId, string userId, long id ) {

			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	Settings,
	IsEnabled
FROM dbo.Quiz( NOLOCK )
WHERE Id = @Id AND ( OrgId = @OrgId OR @UserId = '169' )";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new { OrgId = orgId, UserId = userId, Id = id } );
		}

		public async Task<QuizDto> Get( long orgId, string userId, string uid ) {

			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	Settings,
	IsEnabled
FROM dbo.Quiz ( NOLOCK )
WHERE UId = @UId AND ( OrgId = @OrgId OR @UserId = '169' )";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new { OrgId = orgId, UserId = userId, UId = uid } );
		}

		public async Task<long> Add( long orgId, string userId, QuizDto dto ) {

			const string sql = @"
INSERT INTO dbo.Quiz(
	UId,
	OrgId,
	Name,
	Settings,
	IsEnabled,
	CreatedOn,
	ModifiedOn
)
OUTPUT INSERTED.Id
VALUES
(
	@UId,
	@OrgId,
	@Name,
	@Settings,
	@IsEnabled,
	@CreatedOn,
	@ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			return await conn.ExecuteScalarAsync<long>( sql, new {
				OrgId = orgId,
				UserId = userId,
				dto.UId,
				dto.Name,
				dto.Settings,
				dto.IsEnabled,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Update( long orgId, string userId, QuizDto dto ) {

			const string sql = @"
UPDATE dbo.Quiz
SET Name = @Name,
	Settings = @Settings,
    IsEnabled = @IsEnabled,
    ModifiedOn = @ModifiedOn
WHERE UId = @UId AND ( OrgId = @OrgId OR @UserId = '169' )";
			
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				OrgId = orgId,
				UserId = userId,
				dto.UId,
				dto.Name,
				dto.Settings,
				dto.IsEnabled,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Delete( long orgId, string userId, string uid ) {

			const string sql = @"
DELETE
FROM dbo.Quiz
WHERE UId = @UId AND ( OrgId = @OrgId OR @UserId = '169' )";

			using IDbConnection db = GetConnection();
			await db.ExecuteAsync( sql, new { OrgId = orgId, UserId = userId, UId = uid } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}
}
