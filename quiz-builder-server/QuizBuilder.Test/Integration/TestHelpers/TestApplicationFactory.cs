using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Api;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {

		protected override void ConfigureWebHost( IWebHostBuilder builder ) {

			builder.ConfigureAppConfiguration( config =>
			{
				var integrationConfig = new ConfigurationBuilder()
					.AddJsonFile( "integrationsettings.json" )
					.Build();

				config.AddConfiguration( integrationConfig );
			} );
		}

	}

	internal static class TestApplicationFactoryExtensions {

		public static TestDatabaseWrapper GetTestDatabaseWrapper( this TestApplicationFactory<Startup> factory ) {
			IConfiguration config = factory.Services.GetRequiredService<IConfiguration>();
			return new TestDatabaseWrapper( config );
		}

	}

}
