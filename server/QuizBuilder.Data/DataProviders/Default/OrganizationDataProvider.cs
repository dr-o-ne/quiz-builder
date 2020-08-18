using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	public sealed class OrganizationDataProvider : IOrganizationDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public OrganizationDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<OrganizationDto> Add( OrganizationDto dto ) {

			const string sql = @"

INSERT INTO dbo.Organization (
	UId,
	CreatedOn,
	ModifiedOn
)
OUTPUT
	INSERTED.Id,
	INSERTED.UId,
	INSERTED.CreatedOn,
	INSERTED.ModifiedOn
VALUES (
	@UId,
	@CreatedOn,
	@ModifiedOn
)";

			using IDbConnection conn = GetConnection();
			return await conn.QueryFirstAsync<OrganizationDto>( sql, new {
				dto.UId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );

		}

		public async Task Delete( long id ) {

			const string sql = @"

			DELETE FROM dbo.QuizItem WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { Id = id } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
