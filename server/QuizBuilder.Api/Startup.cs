using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizBuilder.Common;
using QuizBuilder.Data;
using QuizBuilder.Data.Dto;
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

	        services.AddDbContext<UserDbContext>( options => options.UseSqlServer( Configuration.GetConnectionString( "defaultConnectionString" ) ) );

	        services.AddIdentity<UserDto, IdentityRole>( options => { options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider; } )
		        .AddDefaultTokenProviders()
		        .AddEntityFrameworkStores<UserDbContext>();

			ConfigureApplication( services );
		}

        private static void ConfigureApplication( IServiceCollection services ) {

	        services.AddDispatchers();
	        services.AddHandlers();
			services.AddServices();
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

            UpdateDatabase( app );

			app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

		private static void UpdateDatabase( IApplicationBuilder app ) {
			Console.WriteLine( "Creating Database..." );

			using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			using var dbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();

			dbContext.Database.Migrate();

			Console.WriteLine( "Database creation done." );
		}
	}
}
