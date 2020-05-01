using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {

		public readonly TestDatabase DB = new TestDatabase();

		protected override void ConfigureWebHost( IWebHostBuilder builder ) {
			builder.ConfigureServices( services => {
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
