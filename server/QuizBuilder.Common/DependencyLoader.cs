using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Common.CQRS.Dispatchers.Default;
using QuizBuilder.Common.Services;
using QuizBuilder.Common.Services.Default;

namespace QuizBuilder.Common {

	public static class DependencyLoader {

		public static void AddCommon( this IServiceCollection services ) {
			services.AddScoped<ICommandDispatcher, CommandDispatcher>();
			services.AddScoped<IQueryDispatcher, QueryDispatcher>();
			services.AddScoped<IDispatcher, Dispatcher>();

			services.AddSingleton<IUIdService, UIdService>();
		}

	}
}
