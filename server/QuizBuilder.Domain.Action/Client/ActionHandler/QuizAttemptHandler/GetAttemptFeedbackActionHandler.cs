using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetAttemptFeedbackActionHandler : IQueryHandler<GetAttemptFeedbackInfoAction, CommandResult<AttemptFeedbackInfo>> {

		private readonly IMapper _mapper;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetAttemptFeedbackActionHandler(
			IMapper mapper,
			IQuizAttemptDataProvider attemptDataProvider,
			IQuizDataProvider quizDataProvider
		) {
			_mapper = mapper;
			_attemptDataProvider = attemptDataProvider;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult<AttemptFeedbackInfo>> HandleAsync( GetAttemptFeedbackInfoAction action ) {

			AttemptDto attemptDto = await _attemptDataProvider.Get( action.AttemptUId );
			QuizDto quizDto = await _quizDataProvider.Get( 0, "169", attemptDto.QuizUId );
			Quiz quiz = _mapper.Map<Quiz>( quizDto );

			var payload = new AttemptFeedbackInfo();
			if( quiz.IsResultTotalScoreEnabled )
				payload.TotalScore = attemptDto.TotalScore;
			if( quiz.IsResultDurationEnabled )
				payload.Duration = 100;
			if( quiz.IsResultFeedbackEnabled ) {
				if( attemptDto.TotalScore > 0 ) //TODO: > quiz.PassGrade
					payload.Feedback = quiz.ResultPassText;
				else
					payload.Feedback = quiz.ResultFailText;
			}
			 
			return new CommandResult<AttemptFeedbackInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = payload
			};
		}

	}

}
