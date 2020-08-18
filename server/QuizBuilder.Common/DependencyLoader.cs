using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Common.CQRS.Dispatchers.Default;

namespace QuizBuilder.Common {

	public static class DependencyLoader {

		public static void AddDispatchers( this IServiceCollection services ) {
			services.AddScoped<ICommandDispatcher, CommandDispatcher>();
			services.AddScoped<IQueryDispatcher, QueryDispatcher>();
			services.AddScoped<IDispatcher, Dispatcher>();
		}

	}
}
