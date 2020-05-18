using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using Microsoft.Extensions.Configuration;
using ServiceStack.OrmLite;

namespace QuizBuilder.Test.Integration.TestHelpers {

	internal sealed class TestDatabaseWrapper {

		private static readonly ImmutableArray<string> DataTables = new List<string> {
			"dbo.QuizQuizItem",
			"dbo.QuizItem",
			"dbo.Quiz",
			"dbo.Question"
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

		public void Insert<T>( string table, IEnumerable<T> items ) {
			using IDbConnection conn = CreateDbConnection();
			conn.Open();

			conn.ExecuteSql( $"SET IDENTITY_INSERT {table} ON" );
			foreach( var item in items ) 
				conn.Insert( table, item );
			conn.ExecuteSql( $"SET IDENTITY_INSERT {table} OFF" );
		}

	}

	internal static class GenericTableExtensions {

		private static object ExecWithAlias<T>( string table, Func<object> fn ) {
			var modelDef = typeof( T ).GetModelMetadata();
			lock( modelDef ) {
				string hold = modelDef.Alias;
				try {
					modelDef.Alias = table;
					return fn();
				} finally {
					modelDef.Alias = hold;
				}
			}
		}

		public static long Insert<T>( this IDbConnection db, string table, T obj, bool selectIdentity = false ) =>
			(long)ExecWithAlias<T>( table, () => db.Insert( obj, selectIdentity ) );
	}
}
