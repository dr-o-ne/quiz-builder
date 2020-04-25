using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Mapper.Default;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Repository.Repository.Default;

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
			string connectionString = Configuration.GetConnectionString( "defaultConnectionString" );

			services.AddSingleton<IQuizMapper, QuizMapper>();
			services.AddSingleton<IQuestionMapper, QuestionMapper>();

			services.AddSingleton( typeof( IGenericRepository<> ), typeof( GenericRepository<> ) );

			services.AddControllers();
            services.AddDispatchers();
            services.AddHandlers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
