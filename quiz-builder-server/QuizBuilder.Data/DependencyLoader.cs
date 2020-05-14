using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Data.Common;
using QuizBuilder.Data.Common.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.DataProviders.Default;

namespace QuizBuilder.Data {

	public static class DependencyLoader {

		public static void AddData( this IServiceCollection services ) {

			services.AddSingleton<IQuizDataProvider, QuizDataProvider>();
			services.AddSingleton<IQuestionDataProvider, QuestionDataProvider>();
			services.AddSingleton<IStructureDataProvider, StructureDataProvider>();
			services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();


		}

	}

}
