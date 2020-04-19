using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Common.Dispatchers.Default;
using QuizBuilder.Common.Handlers;

namespace QuizBuilder.Common.Extensions
{
    public static class Extensions
    {
        public static void AddDispatchers(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<IDispatcher, Dispatcher>();
        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddCommandQueryHandlers(typeof(ICommandHandler<>));
            services.AddCommandQueryHandlers(typeof(ICommandHandler<,>));
            services.AddCommandQueryHandlers(typeof(IQueryHandler<,>));
        }

        private static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(Extensions).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                );

            foreach (var handler in handlers)
            {
                services.AddScoped(
                    handler.GetInterfaces()
                        .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }
    }
}
