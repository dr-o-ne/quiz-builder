using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetAttemptFeedbackActionHandler : IQueryHandler<GetAttemptInfoAction, CommandResult<AttemptFeedbackInfo>> {

		private readonly IQuizAttemptDataProvider _attemptDataProvider;

		public GetAttemptFeedbackActionHandler( IQuizAttemptDataProvider attemptDataProvider ) {
			_attemptDataProvider = attemptDataProvider;
		}

		public async Task<CommandResult<AttemptFeedbackInfo>> HandleAsync( GetAttemptInfoAction action ) {

			AttemptDto attemptDto = await _attemptDataProvider.Get( action.AttemptUId );
			 

			return new CommandResult<AttemptFeedbackInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = new AttemptFeedbackInfo {TotalScore = attemptDto.TotalScore}
			};
		}

	}

}
