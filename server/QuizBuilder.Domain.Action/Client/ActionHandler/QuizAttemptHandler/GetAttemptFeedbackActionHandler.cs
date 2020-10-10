using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetAttemptFeedbackActionHandler : IQueryHandler<GetAttemptFeedbackInfoAction, CommandResult<AttemptFeedbackInfo>> {

		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetAttemptFeedbackActionHandler(
			IQuizAttemptDataProvider attemptDataProvider,
			IQuizDataProvider quizDataProvider
		) {
			_attemptDataProvider = attemptDataProvider;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult<AttemptFeedbackInfo>> HandleAsync( GetAttemptFeedbackInfoAction action ) {

			AttemptDto attemptDto = await _attemptDataProvider.Get( action.AttemptUId );
			QuizDto quizDto = await _quizDataProvider.Get( 0, "169", attemptDto.QuizUId );



			 
			return new CommandResult<AttemptFeedbackInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = new AttemptFeedbackInfo {TotalScore = attemptDto.TotalScore}
			};
		}

	}

}
