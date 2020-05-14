using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

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
}
