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

		public async Task<ImmutableArray<QuizDto>> GetAll() {

			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	IsEnabled
FROM dbo.Quiz( NOLOCK )";

			using IDbConnection conn = GetConnection();
			IEnumerable<QuizDto> data = await conn.QueryAsync<QuizDto>( sql );

			return data.ToImmutableArray();
		}

		public async Task<QuizDto> Get( long id ) {
			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	IsEnabled
FROM dbo.Quiz( NOLOCK )
WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new {Id = id} );
		}

		public async Task<QuizDto> Get( string uid ) {
			const string sql = @"
SELECT
	Id, 
	UId,
	Name,
	IsEnabled
FROM dbo.Quiz ( NOLOCK )
WHERE UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<QuizDto>( sql, new { UId = uid } );
		}

		public async Task<long> Add( QuizDto dto ) {

			const string sql = @"
INSERT INTO dbo.Quiz(
	UId, 
	Name, 
	IsEnabled,
	CreatedOn,
	ModifiedOn
)
OUTPUT INSERTED.Id
VALUES
(
	@UId, 
	@Name, 
	@IsEnabled,
	@CreatedOn,
	@ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			return await conn.ExecuteScalarAsync<long>( sql, new {dto.UId, dto.Name, dto.IsEnabled, CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow} );
		}

		public async Task Update( QuizDto dto ) {

			const string sql = @"
UPDATE dbo.Quiz
SET Name = @Name,
    IsEnabled = @IsEnabled,
    ModifiedOn = @ModifiedOn
WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { dto.Id, dto.Name, dto.IsEnabled, ModifiedOn = DateTime.UtcNow } );
		}

		public async Task Delete( string uid ) {
			using IDbConnection db = GetConnection();
			await db.ExecuteAsync( "DELETE FROM dbo.Quiz WHERE UId=@UId", new { UId = uid } );
		}

		public async Task Delete( List<string> uids ) {
			using IDbConnection db = GetConnection();
			await db.ExecuteAsync( "DELETE FROM dbo.Quiz WHERE UId IN @UIds", new { UIds = uids } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}
}
