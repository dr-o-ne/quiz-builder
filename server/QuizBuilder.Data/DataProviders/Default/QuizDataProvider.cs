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
FROM dbo.Quiz( NOLOCK )";

			using IDbConnection conn = GetConnection();
			IEnumerable<QuizDto> data = await conn.QueryAsync<QuizDto>( sql );

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
WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new {Id = id} );
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
WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new { UId = uid } );
		}

		public async Task<long> Add( long orgId, string userId, QuizDto dto ) {

			const string sql = @"
INSERT INTO dbo.Quiz(
	UId, 
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
	@Name,
	@Settings,
	@IsEnabled,
	@CreatedOn,
	@ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			return await conn.ExecuteScalarAsync<long>( sql, new {
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
WHERE UId = @UId";
			
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				dto.UId,
				dto.Name,
				dto.Settings,
				dto.IsEnabled,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task Delete( long orgId, string userId, string uid ) {
			using IDbConnection db = GetConnection();
			await db.ExecuteAsync( "DELETE FROM dbo.Quiz WHERE UId=@UId", new { UId = uid } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}
}
