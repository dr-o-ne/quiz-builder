using System.Collections.Immutable;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class QuestionCommandResult : CommandResult {

		public QuestionViewModel Question { get; set; }

	}

	public sealed class QuestionQueryResult {

		public QuestionViewModel Question { get; set; }

	}

}
