using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Model.Model;

namespace QuizBuilder.Model.Extensions
{
    public static class Extensions
    {
        public static void AddQuizBuilderDataContext(this IServiceCollection services)
        {
            services.AddDbContext<QuizBuilderDataContext>(options =>
                options.UseSqlite(
                    @"Data Source=..\QuizBuilder.db",
                    b => b.MigrationsAssembly("QuizBuilder.Model"))
                );
        }
    }
}
