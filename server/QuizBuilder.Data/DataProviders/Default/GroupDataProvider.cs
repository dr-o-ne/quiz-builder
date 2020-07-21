using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

		public async Task<ImmutableArray<GroupDto>> GetByQuiz( string uid ) {

			const string sql = @"
				SELECT
					qi.Id,
					qi.UId,
					qi.Name,
					qi.SortOrder
				FROM
					dbo.QuizItem qi WITH(NOLOCK)
				INNER JOIN dbo.QuizQuizItem qqi WITH(NOLOCK)
					ON qi.Id = qqi.QuizItemId
				INNER JOIN dbo.Quiz qz WITH(NOLOCK)
					ON qz.Id = qqi.QuizId
				WHERE
					qi.TypeId = 2 AND qz.UId = @UId";

			using IDbConnection conn = GetConnection();
			IEnumerable<GroupDto> data = await conn.QueryAsync<GroupDto>( sql, new { UId = uid } );

			return data.ToImmutableArray();
		}

		public async Task<GroupDto> Get( string uid ) {

			const string sql = @"
				SELECT
					qi.Id,
				    qi.Uid,
					qi.Name,
					qi.SortOrder
				FROM
					dbo.QuizItem qi WITH(NOLOCK)
				WHERE
					qi.TypeId = 2 AND qi.UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.QuerySingleOrDefaultAsync<GroupDto>( sql, new { UId = uid } );
		}

		public async Task<long> Add( long quizId, GroupDto dto ) {

			const string sql = @"
				INSERT INTO dbo.QuizItem (
					UId,
				    TypeId,
				    ParentId,
				    QuestionId,
					Name,
					SortOrder,
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
					1 + (
						SELECT ISNULL(MAX(SortOrder), 0) FROM dbo.QuizItem AS qi
						INNER JOIN dbo.QuizQuizItem AS qqi ON qqi.QuizItemId = qi.id
						WHERE 
							qi.TypeId = 2 AND
							QuizId = @QuizId ),
				    @CreatedOn,
				    @ModifiedOn
				)";
		
					using IDbConnection conn = GetConnection();
					return await conn.ExecuteScalarAsync<long>( sql, new {
						dto.UId,
						dto.Name,
						QuizId = quizId,
						CreatedOn = DateTime.UtcNow,
						ModifiedOn = DateTime.UtcNow
					} );
				}

		public async Task<int> Update( GroupDto dto ) {
			const string sql = @"
				UPDATE
					dbo.QuizItem
				SET
					Name = @Name,
					SortOrder = @SortOrder,
					ModifiedOn = @ModifiedOn
				WHERE Id = @Id";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteAsync( sql, new {
				dto.Id,
				dto.Name,
				dto.SortOrder,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task<int> Delete( string uid ) {

			const string sql = @"
				DELETE
				FROM
				     dbo.QuizItem
				WHERE
				      UId = @UId";

			using IDbConnection conn = GetConnection();
			return await conn.ExecuteAsync( sql, new { UId = uid } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
