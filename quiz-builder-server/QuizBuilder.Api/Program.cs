using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuizBuilder.Model.Model;
using System.Linq;
using QuizBuilder.Model.Model.Default;

namespace QuizBuilder.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<QuizBuilderDataContext>();
                    context.Database.Migrate();
                    SeedData(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        private static void SeedData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<QuizBuilderDataContext>();
            context.Database.EnsureCreated();
            if (!context.Quizzes.Any())
            {
                for (int i = -11; i < 0; i++)
                {
                    context.Quizzes.Add(new Quiz {Id = i, Name = $"Quiz {i}"});
                }
                context.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
