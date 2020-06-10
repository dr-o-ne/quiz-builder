using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Graders;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model {

	public static class DependencyLoader {

		public static void AddModels( this IServiceCollection services ) {

			services.AddSingleton<IQuestionGrader<MultipleChoiceQuestion, MultipleChoiceAnswer>, MultipleChoiceGrader>();
			services.AddSingleton<IQuestionGrader<MultipleSelectQuestion, MultipleSelectAnswer>, MultipleSelectGrader>();
			services.AddSingleton<IQuestionGrader<TrueFalseQuestion, TrueFalseAnswer>, TrueFalseGrader>();
		}

	}
}
