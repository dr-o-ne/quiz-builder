using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Utils.Services;
using QuizBuilder.Utils.Services.Default;

namespace QuizBuilder.Utils {

	public static class DependencyLoader {

		public static void AddUtils( this IServiceCollection services ) {
			services.AddSingleton<IUIdService, UIdService>();
		}

	}

}
