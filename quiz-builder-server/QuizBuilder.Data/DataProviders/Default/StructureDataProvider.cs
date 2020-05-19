﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Data.Common;

namespace QuizBuilder.Data.DataProviders.Default {

	internal sealed class StructureDataProvider : IStructureDataProvider {

		private readonly IDatabaseConnectionFactory _dbConnectionFactory;

		public StructureDataProvider( IDatabaseConnectionFactory dbConnectionFactory ) {
			_dbConnectionFactory = dbConnectionFactory;
		}

		public async Task AddQuizQuestionRelationship( long quizId, long quizItemId ) {

			const string sql = @"
INSERT INTO dbo.QuizQuizItem (
	QuizId,
    QuizItemId,
    CreatedOn,
    ModifiedOn
)
VALUES (
	@QuizId,
    @QuizItemId,
    @CreatedOn,
    @ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				QuizId = quizId,
				QuizItemId = quizItemId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task AddQuizGroupRelationship( long quizId, long groupId ) {
			const string sql = @"
INSERT INTO dbo.QuizQuizItem (
	QuizId,
    QuizItemId,
    CreatedOn,
    ModifiedOn
)
VALUES (
	@QuizId,
    @QuizItemId,
    @CreatedOn,
    @ModifiedOn
)";
			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new {
				QuizId = quizId,
				QuizItemId = groupId,
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow
			} );
		}

		public async Task AddGroupQuestionRelationship( long groupId, long questionId ) {
			const string sql = @"
		UPDATE
			dbo.QuizItem
		SET
			ParentId = @GroupId
		WHERE
			Id = @Id";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { GroupId = groupId, Id = questionId } );
		}

		public async Task DeleteQuizQuestionRelationship( string quizUId, string quizItemUId ) {
			const string sql = @"
DELETE qqi 
FROM dbo.QuizQuizItem qqi
INNER JOIN dbo.Quiz q ON q.Id = qqi.QuizId
INNER JOIN dbo.QuizItem qi ON qi.Id = qqi.QuizItemId
WHERE 
	q.UId = @QuizUId AND
	qi.UId = @QuizItemId";

			using IDbConnection conn = GetConnection();
			await conn.ExecuteAsync( sql, new { QuizUId = quizUId, QuizItemId = quizItemUId } );
		}

		public async Task<IEnumerable<(string, int)>> DeleteQuizRelationships( string quizUId ) {
			const string sql = @"
DECLARE @QuizQuizItemIds TABLE
( Id BIGINT )

DELETE qqi
OUTPUT DELETED.Id INTO @QuizQuizItemIds
FROM dbo.QuizQuizItem qqi
INNER JOIN dbo.Quiz q ON q.Id = qqi.QuizId
WHERE q.UId = @QuizUId

SELECT UId, q.TypeId
FROM dbo.QuizItem q
INNER JOIN @QuizQuizItemIds AS temp ON temp.Id = q.Id
";

			using IDbConnection conn = GetConnection();
			return await conn.QueryAsync<(string, int)>( sql, new { QuizUId = quizUId } );
		}

		private IDbConnection GetConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}
	}

}
