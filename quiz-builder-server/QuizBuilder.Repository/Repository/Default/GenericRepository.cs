using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Repository.Repository.Default {
	public class GenericRepository<T> : IGenericRepository<T> where T : class {
		private static readonly List<string> NonUpdateableColumns = new List<string> {"Id", "CreatedOn"};

		private readonly string _connectionString;
		private readonly string _tableName;

		public GenericRepository( IConfiguration config ) {
			_connectionString = config.GetConnectionString( "defaultConnectionString" );
			_tableName = GetTableName;
		}

		private IDbConnection CreateConnection() {
			SqlConnection conn = new SqlConnection( _connectionString );
			conn.Open();
			return conn;
		}

		private static IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public );

		private static string GetTableName => typeof(T).GetAttributeValue( ( TableAttribute attribute ) => attribute.Name );

		public async Task<IEnumerable<T>> GetAllAsync() {
			using IDbConnection connection = CreateConnection();
			return await connection.QueryAsync<T>( $"SELECT * FROM {_tableName}" );
		}

		public async Task<T> GetByIdAsync( long id ) {
			using IDbConnection connection = CreateConnection();
			T result = await connection.QuerySingleOrDefaultAsync<T>( $"SELECT * FROM {_tableName} WHERE Id=@Id", new {Id = id} );
			return result;
		}

		public async Task<int> AddAsync( T entity ) {
			using IDbConnection connection = CreateConnection();
			string insertQuery = GenerateInsertQuery();
			return await connection.ExecuteAsync( insertQuery, entity );
		}

		public async Task<int> UpdateAsync( T entity ) {
			using IDbConnection connection = CreateConnection();
			string updateQuery = GenerateUpdateQuery();
			return await connection.ExecuteAsync( updateQuery, entity );
		}

		public async Task<int> DeleteAsync( long id ) {
			using IDbConnection db = CreateConnection();
			return await db.ExecuteAsync( $"DELETE FROM {_tableName} WHERE Id=@Id", new {Id = id} );
		}

		private static List<string> GenerateListOfProperties( IEnumerable<PropertyInfo> listOfProperties ) =>
			listOfProperties
				.Where( p => p.GetCustomAttributes( typeof(IgnoreDataMemberAttribute), false ).Length == 0 )
				.Select( x => x.Name )
				.ToList();

		protected virtual string GenerateInsertQuery() {
			var insertQuery = new StringBuilder( $"INSERT INTO {_tableName} " );

			insertQuery.Append( "(" );
			List<string> properties = GenerateListOfProperties( GetProperties );
			properties.ForEach( prop => { insertQuery.Append( $"[{prop}]," ); } );

			insertQuery
				.Remove( insertQuery.Length - 1, 1 )
				.Append( ") VALUES (" );

			properties.ForEach( prop => { insertQuery.Append( $"@{prop}," ); } );

			return insertQuery
				.Remove( insertQuery.Length - 1, 1 )
				.Append( ")" )
				.ToString();
		}

		protected virtual string GenerateUpdateQuery() {
			var updateQuery = new StringBuilder( $"UPDATE {_tableName} SET " );
			var properties = GenerateListOfProperties( GetProperties );

			foreach( string property in properties ) {
				if( NonUpdateableColumns.Contains( property ) )
					continue;

				updateQuery.Append( $"{property}=@{property}," );
			}

			return updateQuery
				.Remove( updateQuery.Length - 1, 1 )
				.Append( " WHERE Id=@Id" )
				.ToString();
		}
	}
}
