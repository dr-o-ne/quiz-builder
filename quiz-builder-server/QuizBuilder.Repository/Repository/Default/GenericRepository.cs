using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QuizBuilder.Repository.Repository.Default {

	public class GenericRepository<T> : IGenericRepository<T> where T : class {

		private readonly string _connectionString;
		private readonly string _tableName;

		public GenericRepository( string connectionString, string tableName ) {
			_connectionString = connectionString;
			_tableName = tableName;
		}
		
		private IDbConnection CreateConnection() {
			SqlConnection conn = new SqlConnection( _connectionString );
			conn.Open();
			return conn;
		}

		private static IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties(BindingFlags.Public);

		public async Task<IEnumerable<T>> GetAllAsync() {
			using IDbConnection connection = CreateConnection();
			return await connection.QueryAsync<T>( $"SELECT * FROM {_tableName}" );
		}

		public async Task<T> GetByIdAsync( long id ) {
			using IDbConnection connection = CreateConnection();
			T result = await connection.QuerySingleOrDefaultAsync<T>( $"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id } );
			if( result == null )
				throw new KeyNotFoundException( $"{_tableName} with id [{id}] could not be found." );
			return result;
		}

		public async Task<long> AddAsync( T entity ) {
			using IDbConnection connection = CreateConnection();
			string insertQuery = GenerateInsertQuery();
			return await connection.QuerySingleOrDefaultAsync<long>( insertQuery, entity );
		}

		public async Task UpdateAsync( T entity ) {
			using IDbConnection connection = CreateConnection();
			string updateQuery = GenerateUpdateQuery();
			await connection.ExecuteAsync( updateQuery, entity );
		}

		public async Task DeleteAsync( long id ) {
			using IDbConnection db = CreateConnection();
			await db.ExecuteAsync( $"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id } );
		}

		private static List<string> GenerateListOfProperties( IEnumerable<PropertyInfo> listOfProperties ) {
			return ( from prop in listOfProperties let attributes = prop.GetCustomAttributes( typeof( DescriptionAttribute ), false )
					 where attributes.Length <= 0 || ( attributes[0] as DescriptionAttribute )?.Description != "ignore" select prop.Name ).ToList();
		}

		protected virtual string GenerateInsertQuery() {
			var insertQuery = new StringBuilder( $"INSERT INTO {_tableName} " );

			insertQuery.Append( "(" );

			List<string> properties = GenerateListOfProperties( GetProperties );
			properties.ForEach( prop => { insertQuery.Append( $"[{prop}]," ); } );

			insertQuery
				.Remove( insertQuery.Length - 1, 1 )
				.Append( ") VALUES (" );

			properties.ForEach( prop => { insertQuery.Append( $"@{prop}," ); } );

			insertQuery
				.Remove( insertQuery.Length - 1, 1 )
				.Append( ")" );

			return insertQuery.ToString();
		}

		protected virtual string GenerateUpdateQuery() {
			var updateQuery = new StringBuilder( $"UPDATE {_tableName} SET " );
			var properties = GenerateListOfProperties( GetProperties );

			properties.ForEach( property => {
				if( !property.Equals( "Id" ) ) {
					updateQuery.Append( $"{property}=@{property}," );
				}
			} );

			updateQuery.Remove( updateQuery.Length - 1, 1 );
			updateQuery.Append( " WHERE Id=@Id" );

			return updateQuery.ToString();
		}
	}
}
