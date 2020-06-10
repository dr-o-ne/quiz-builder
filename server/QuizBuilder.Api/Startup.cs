using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizBuilder.Common;
using QuizBuilder.Data;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model;
using QuizBuilder.Utils;

namespace QuizBuilder.Api {

	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
	        services.Configure<RouteOptions>( options => {
		        options.LowercaseUrls = true;
		        options.LowercaseQueryStrings = true;
		        options.AppendTrailingSlash = true;
	        } );

	        services.AddControllers().AddJsonOptions( options => {
		        options.JsonSerializerOptions.IgnoreNullValues = true;
		        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
	        } );

			ConfigureApplication( services );
		}

        private static void ConfigureApplication( IServiceCollection services ) {
	        services.AddDispatchers();
	        services.AddHandlers();
	        services.AddMappers();
	        services.AddModels();
	        services.AddUtils();
	        services.AddData();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors( x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
