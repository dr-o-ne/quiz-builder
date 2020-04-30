using System;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestDatabase {

		public readonly OrmLiteConnectionFactory ConnectionFactory = new OrmLiteConnectionFactory( ":memory:", SqliteOrmLiteDialectProvider.Instance );

	}

	public static class GenericTableExtensions {

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

		public static void DropAndCreateTable<T>( this IDbConnection db, string table ) =>
			ExecWithAlias<T>( table, () => {
				db.DropAndCreateTable<T>();
				return null;
			} );

		public static long Insert<T>( this IDbConnection db, string table, T obj, bool selectIdentity = false ) =>
			(long)ExecWithAlias<T>( table, () => db.Insert( obj, selectIdentity ) );
	}
}
