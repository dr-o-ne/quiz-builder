using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Dto;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class GroupDataProvider : IGroupDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public GroupDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task<long> Add( GroupDto dto ) {

			const string sql = @"

		INSERT INTO dbo.QuizItem (
			UId,
		    TypeId,
		    ParentId,
		    QuestionId,
			Name,
		    CreatedOn,
		    ModifiedOn
		)
		OUTPUT INSERTED.Id
		VALUES(	
			@UId,
		    2,
		    NULL,
		    NULL,
			@Name,
		    @CreatedOn,
		    @ModifiedOn
		)";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteScalarAsync<long>( sql, new {
				dto.UId,
				dto.Name,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public Task Update( GroupDto dto ) {
			throw new System.NotImplementedException();
		}

		public Task Delete( string uid ) {
			throw new System.NotImplementedException();
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
