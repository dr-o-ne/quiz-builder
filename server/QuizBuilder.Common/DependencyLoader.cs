using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Common.Dispatchers.Default;

namespace QuizBuilder.Common {

	public static class DependencyLoader {

		public static void AddDispatchers( this IServiceCollection services ) {
			services.AddScoped<ICommandDispatcher, CommandDispatcher>();
			services.AddScoped<IQueryDispatcher, QueryDispatcher>();
			services.AddScoped<IDispatcher, Dispatcher>();
		}

	}
}
