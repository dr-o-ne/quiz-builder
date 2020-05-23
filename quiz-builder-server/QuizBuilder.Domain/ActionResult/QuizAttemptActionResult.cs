using System.Collections.Immutable;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult {

	public sealed class QuizAttemptCommandResult : CommandResult {

		public QuizAttemptViewModel QuizAttempt { get; set; }

		public ImmutableList<QuestionViewModel> Questions { get; set; }

	}

}
