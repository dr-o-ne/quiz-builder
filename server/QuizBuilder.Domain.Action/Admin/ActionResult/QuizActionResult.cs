using System.Collections.Immutable;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class QuizCommandResult : CommandResult {

		public QuizViewModel Quiz { get; set; }

	}

	public sealed class QuizzesQueryResult {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
