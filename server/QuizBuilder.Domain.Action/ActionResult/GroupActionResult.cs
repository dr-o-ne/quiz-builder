using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.ActionResult {

	public sealed class GroupCommandResult : CommandResult {

		public GroupViewModel Group { get; set; }

	}

}
