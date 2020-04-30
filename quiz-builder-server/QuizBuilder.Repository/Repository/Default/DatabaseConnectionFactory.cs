using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuizBuilder.Repository.Repository.Default {

	internal sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory {

		private readonly string _connectionString;

		public DatabaseConnectionFactory( IConfiguration config ) {
			_connectionString = config.GetConnectionString( "defaultConnectionString" );
		}

		public IDbConnection GetConnection() {
			return new SqlConnection( _connectionString );
		}

	}

}
