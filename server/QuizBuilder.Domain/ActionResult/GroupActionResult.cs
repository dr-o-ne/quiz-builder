using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult {

	public sealed class GroupCommandResult : CommandResult {

		public GroupViewModel Group { get; set; }

	}

}
