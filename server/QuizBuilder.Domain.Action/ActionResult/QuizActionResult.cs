using System.Collections.Immutable;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.ActionResult {

	public sealed class QuizCommandResult : CommandResult {

		public QuizViewModel Quiz { get; set; }

	}

	public sealed class QuizQueryResult {

		public QuizViewModel Quiz { get; set; }

	}

	public sealed class QuizzesQueryResult {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
