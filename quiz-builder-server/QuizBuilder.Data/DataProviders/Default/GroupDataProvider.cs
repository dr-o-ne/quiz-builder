using System;
using System.Collections.Generic;
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

		public async Task Delete( string uid ) {
			const string sql =
				@"DELETE FROM dbo.QuizItem WHERE UId=@UId";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { UId = uid } );
		}

		public async Task<GroupDto> Get( string uid ) {
			const string sql = @"

		SELECT
			qi.Id,
		    qi.Uid,
			qi.Name
		FROM
			dbo.QuizItem qi WITH(NOLOCK)
		WHERE
			qi.TypeId = 2 AND qi.UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<GroupDto>( sql, new { UId = uid } );
		}

		public async Task<IEnumerable<GroupDto>> GetByQuiz( string uid ) {
			const string sql = @"
		SELECT
			qi.UId,
			qi.Name
		FROM
			dbo.QuizItem qi WITH(NOLOCK)
		INNER JOIN dbo.QuizQuizItem qqi WITH(NOLOCK)
			ON qi.Id = qqi.QuizItemId
		INNER JOIN dbo.Quiz qz WITH(NOLOCK)
			ON qz.Id = qqi.QuizId
		WHERE
			qi.TypeId = 2 AND qz.UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<GroupDto>( sql, new { UId = uid } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
