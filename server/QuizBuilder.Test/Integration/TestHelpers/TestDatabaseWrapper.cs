using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using Dapper;
using QuizBuilder.Data.Common;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestDatabaseWrapper {

		private static readonly ImmutableArray<string> DataTables = new List<string> {
			"dbo.QuizQuizItem",
			"dbo.QuizItem",
			"dbo.Quiz",
			"dbo.Attempt"
		}.ToImmutableArray();

		private readonly IDatabaseConnectionFactory _connectionFactory;

		public TestDatabaseWrapper( IDatabaseConnectionFactory connectionFactory ) {
			_connectionFactory = connectionFactory;
		}

		public void Cleanup() {
			using IDbConnection conn = _connectionFactory.GetConnection();
			conn.Open();
			foreach( string dataTable in DataTables )
				conn.Execute( "DELETE FROM " + dataTable );
		}

	}

}
