using System.Collections.Immutable;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class QuizzesQueryResult {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
