using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using QuizBuilder.Common;
using QuizBuilder.Data;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model;

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

	        services.Configure<IdentityOptions>( options => {
		        options.Password.RequiredLength = 1;
		        options.Password.RequireDigit = false;
		        options.Password.RequireNonAlphanumeric = false;
		        options.Password.RequireUppercase = false;
		        options.Password.RequireLowercase = false;
	        } );

	        var key = Encoding.ASCII.GetBytes( Consts.JwtSecret );
	        services.AddAuthentication( x => {
			        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		        } )
		        .AddJwtBearer( x => {
			        //x.RequireHttpsMetadata = false;
			        x.SaveToken = true;
			        x.TokenValidationParameters = new TokenValidationParameters {
				        ValidateIssuerSigningKey = true,
				        IssuerSigningKey = new SymmetricSecurityKey( key ),
				        ValidateIssuer = false,
				        ValidateAudience = false
			        };
		        } );

	        services.AddSwaggerGen();

			ConfigureApplication( services );
		}

        private static void ConfigureApplication( IServiceCollection services ) {

	        services.AddCommon();
	        services.AddHandlers();
			services.AddServices();
	        services.AddMappers();
	        services.AddModels();
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

            app.UseSwagger();
            app.UseSwaggerUI( x => {
	            x.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" );
            } );

			app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

		private static void UpdateDatabase( IApplicationBuilder app ) {
			using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			using var dbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();

			dbContext.Database.Migrate();
		}
	}
}
