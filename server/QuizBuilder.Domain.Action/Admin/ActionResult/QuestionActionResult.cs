using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class QuestionCommandResult : CommandResult {

		public QuestionViewModel Question { get; set; }

	}

}
