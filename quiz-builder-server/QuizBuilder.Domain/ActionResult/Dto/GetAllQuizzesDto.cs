using System.Collections.Immutable;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult.Dto {

	public sealed class GetAllQuizzesDto {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
