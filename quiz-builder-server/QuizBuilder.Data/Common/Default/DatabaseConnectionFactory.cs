using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuizBuilder.Data.Common.Default {

	internal sealed class DatabaseConnectionFactory {

		private readonly string _connectionString;

		public DatabaseConnectionFactory( IConfiguration config ) =>
			_connectionString = config.GetConnectionString( "defaultConnectionString" );

		public IDbConnection GetConnection() => new SqlConnection( _connectionString );
	}

}
