using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetAttemptFeedbackActionHandler : IQueryHandler<GetAttemptInfoAction, CommandResult<AttemptFeedbackInfo>> {

		public Task<CommandResult<AttemptFeedbackInfo>> HandleAsync( GetAttemptInfoAction query ) {
			throw new System.NotImplementedException();
		}

	}

}
