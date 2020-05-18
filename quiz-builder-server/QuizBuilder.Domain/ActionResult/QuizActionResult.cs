using System.Collections.Immutable;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult {

	public sealed class QuizCommandResult : CommandResult {

		public QuizQueryResult Data { get; set; }

	}

	public sealed class QuizQueryResult {

		public QuizViewModel Quiz { get; set; }

	}

	public sealed class QuizzesQueryResult {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
