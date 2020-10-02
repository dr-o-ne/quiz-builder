using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetQuizInfoQueryHandler : IQueryHandler<GetQuizInfoAction, CommandResult<StartPageInfo>> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetQuizInfoQueryHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult<StartPageInfo>> HandleAsync( GetQuizInfoAction action ) {
			string quizUId = action.QuizUId;

			if( string.IsNullOrWhiteSpace( quizUId ) ) {
				return new CommandResult<StartPageInfo> {IsSuccess = false};
			}

			QuizDto quizDto = await _quizDataProvider.Get( Consts.SupportUser.OrgId, Consts.SupportUser.UserId, quizUId );

			if( quizDto == null ) {
				return new CommandResult<StartPageInfo> {IsSuccess = false};
			}

			Quiz quiz = _mapper.Map<Quiz>( quizDto );

			if( !quiz.IsValid() ) {
				return new CommandResult<StartPageInfo> {IsSuccess = false};
			}

			if( !quiz.IsAvailable() ) {
				return new CommandResult<StartPageInfo> {IsSuccess = false};
			}

			//TODO: load from DB
			StartPageInfo payload = new StartPageInfo();
			payload.ShowStartPage = true;
			payload.Name = quiz.Name;
			payload.IntroductionText = quiz.Introduction;
			payload.TotalAttempts = 10;
			payload.PassingScore = 80;
			payload.StartButtonText = "Start Quiz";
			payload.TimeLimit = new TimeSpan( 0, 1, 30, 0 );
			payload.TotalQuestions = 10;
			payload.TotalPoints = 100;

			return new CommandResult<StartPageInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = payload
			};
		}
	}
}
