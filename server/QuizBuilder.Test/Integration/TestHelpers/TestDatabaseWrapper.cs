using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using Microsoft.Extensions.Configuration;
using ServiceStack.OrmLite;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestDatabaseWrapper {

		private static readonly ImmutableArray<string> DataTables = new List<string> {
			"dbo.QuizQuizItem",
			"dbo.QuizItem",
			"dbo.Quiz",
			"dbo.Attempt"
		}.ToImmutableArray();

		private readonly OrmLiteConnectionFactory _connectionFactory;

		public TestDatabaseWrapper( IConfiguration config ) {
			string connectionString = config.GetConnectionString( "defaultConnectionString" );
			_connectionFactory = new OrmLiteConnectionFactory(
				connectionString,
				SqlServerDialect.Provider );
		}

		public IDbConnection CreateDbConnection() => _connectionFactory.CreateDbConnection();

		public void Cleanup() {
			using IDbConnection conn = CreateDbConnection();
			conn.Open();
			foreach( string dataTable in DataTables )
				conn.ExecuteSql( "DELETE FROM " + dataTable );
		}

	}

}
