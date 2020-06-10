using System.Collections.Immutable;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.ActionResult {

	public sealed class StartQuizAttemptCommandResult : CommandResult {

		public QuizAttemptViewModel QuizAttempt { get; set; }

		public ImmutableList<QuestionViewModel> Questions { get; set; }

	}

	public sealed class EndQuizAttemptCommandResult : CommandResult {

		public double Score { get; set; }

	}

}
