using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Common.Dispatchers.Default;

namespace QuizBuilder.Common.Extensions {
	public static class Extensions {
		public static void AddDispatchers( this IServiceCollection services ) {
			services.AddScoped<ICommandDispatcher, CommandDispatcher>();
			services.AddScoped<IQueryDispatcher, QueryDispatcher>();
			services.AddScoped<IDispatcher, Dispatcher>();
		}
	}
}
