using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizBuilder.Repository.Repository;
using Dapper;

namespace QuizBuilder.Test.Integration.TestHelpers {
	public class SqliteOrmLiteGuidTypeHandler : SqlMapper.TypeHandler<Guid> {
		public override Guid Parse( object value ) {
			byte[] inVal = System.Text.Encoding.UTF8.GetBytes( value?.ToString() ?? string.Empty );
			byte[] outVal = {inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15]};
			return new Guid( outVal );
		}

		public override void SetValue( System.Data.IDbDataParameter parameter, Guid value ) {
			var inVal = value.ToByteArray();
			byte[] outVal = new byte[] {inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15]};
			parameter.Value = outVal;
		}
	}

	public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
		public readonly TestDatabase DB = new TestDatabase();

		protected override void ConfigureWebHost( IWebHostBuilder builder ) {
			builder.ConfigureServices( services => {
				SqlMapper.AddTypeHandler( new SqliteOrmLiteGuidTypeHandler() );
				SqlMapper.RemoveTypeMap( typeof(Guid) );
				SqlMapper.RemoveTypeMap( typeof(Guid?) );
				AddDatabaseConnectionFactory( services );
				var sp = services.BuildServiceProvider();

				using var scope = sp.CreateScope();
			} );
		}

		private void AddDatabaseConnectionFactory( IServiceCollection services ) {
			var descriptor = services.Single( d => d.ServiceType == typeof(IDatabaseConnectionFactory) );
			services.Remove( descriptor );

			var databaseMock = Mock.Of<IDatabaseConnectionFactory>( x => x.GetConnection() == DB.ConnectionFactory.CreateDbConnection() );
			services.AddSingleton( databaseMock );
		}
	}
}
