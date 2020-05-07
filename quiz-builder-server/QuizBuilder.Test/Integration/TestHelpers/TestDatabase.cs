using System;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using static System.Text.Encoding;
using static Dapper.SqlMapper;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestDatabase {

		//https://stackoverflow.com/questions/25568657/dapper-typehandler-setvalue-not-being-called
		public sealed class SqliteOrmLiteGuidTypeHandler : TypeHandler<Guid> {

			public override Guid Parse( object value ) {
				byte[] inVal = UTF8.GetBytes( value?.ToString() ?? string.Empty );
				byte[] outVal = { inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15] };
				return new Guid( outVal );
			}

			public override void SetValue( IDbDataParameter parameter, Guid value ) {
				var inVal = value.ToByteArray();
				byte[] outVal = { inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15] };
				parameter.Value = outVal;
			}
		}

		static TestDatabase() {
			AddTypeHandler( new SqliteOrmLiteGuidTypeHandler() );
			RemoveTypeMap( typeof( Guid ) );
			RemoveTypeMap( typeof( Guid? ) );
		}

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
