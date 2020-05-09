using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Dapper;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Repository.Repository.Default {

	public class GenericRepository<T> : IGenericRepository<T> where T : class {

		private readonly string _tableName;
		private readonly IDatabaseConnectionFactory _dbConnectionFactory;
		private readonly IGenericQueryBuilder<T> _queryBuilder;

		public GenericRepository( IGenericQueryBuilder<T> queryBuilder, IDatabaseConnectionFactory dbConnectionFactory ) {
			_queryBuilder = queryBuilder;
			_dbConnectionFactory = dbConnectionFactory;
			_tableName = GetTableName;
		}

		private IDbConnection CreateConnection() {
			IDbConnection conn = _dbConnectionFactory.GetConnection();
			conn.Open();
			return conn;
		}

		private static string GetTableName => typeof( T ).GetAttributeValue( ( TableAttribute attribute ) => attribute.Name );

		public async Task<IEnumerable<T>> GetAllAsync() {
			using IDbConnection connection = CreateConnection();
			return await connection.QueryAsync<T>( $"SELECT * FROM {_tableName}" );
		}

		public async Task<T> GetByIdAsync( long id ) {
			using IDbConnection connection = CreateConnection();
			T result = await connection.QuerySingleOrDefaultAsync<T>( $"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id } );
			return result;
		}

		public async Task<int> AddAsync( T entity ) {
			if( entity is null ) {
				return 0;
			}
			using IDbConnection connection = CreateConnection();
			string insertQuery = GenerateInsertQuery();
			return await connection.ExecuteAsync( insertQuery, entity );
		}

		public async Task<int> UpdateAsync( T entity ) {
			if( entity is null ) {
				return 0;
			}
			using IDbConnection connection = CreateConnection();
			string updateQuery = GenerateUpdateQuery();
			return await connection.ExecuteAsync( updateQuery, entity );
		}

		public async Task<int> DeleteAsync( long id ) {
			using IDbConnection db = CreateConnection();
			return await db.ExecuteAsync( $"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id } );
		}

		public async Task<int> DeleteBulkAsync( List<long> ids ) {
			using IDbConnection db = CreateConnection();
			return await db.ExecuteAsync( $"DELETE FROM {_tableName} WHERE Id IN @Ids", new { Ids = ids } );
		}

		protected virtual string GenerateInsertQuery() => _queryBuilder.GetInsertQuery();

		protected virtual string GenerateUpdateQuery() => _queryBuilder.GetUpdateQuery();
	}
}
