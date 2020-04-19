using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Repository.Extensions
{
    public static class Extensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var repositories = typeof(Extensions).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IGenericRepository<>))
                );

            foreach (var repository in repositories)
            {
                services.AddScoped(repository.GetInterfaces().Single(x =>
                    x.GetInterfaces().Any(q =>
                        q.IsGenericType && q.GetGenericTypeDefinition() == typeof(IGenericRepository<>))), repository);
            }
        }
    }
}
