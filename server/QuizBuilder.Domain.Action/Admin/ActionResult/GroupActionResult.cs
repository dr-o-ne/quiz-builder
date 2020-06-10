using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class GroupCommandResult : CommandResult {

		public GroupViewModel Group { get; set; }

	}

}
