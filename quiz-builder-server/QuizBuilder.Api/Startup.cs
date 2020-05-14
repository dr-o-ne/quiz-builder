using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Data;
using QuizBuilder.Domain.Extensions;
using QuizBuilder.Repository.Extensions;
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

			services.AddControllers();
            services.AddDispatchers();
            services.AddHandlers();
            services.AddMappers();
            services.AddRepositories();
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
