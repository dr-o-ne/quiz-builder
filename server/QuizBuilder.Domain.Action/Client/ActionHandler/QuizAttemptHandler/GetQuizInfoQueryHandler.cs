using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Action.Client.Services;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class GetQuizInfoQueryHandler : IQueryHandler<GetQuizInfoAction, CommandResult<StartPageInfo>> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IPageInfoDataFactory _pageInfoDataFactory;

		public GetQuizInfoQueryHandler(
			IMapper mapper,
			IQuizDataProvider quizDataProvider,
			IGroupDataProvider groupDataProvider,
			IQuestionDataProvider questionDataProvider,
			IPageInfoDataFactory pageInfoDataFactory
		) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_groupDataProvider = groupDataProvider;
			_questionDataProvider = questionDataProvider;
			_pageInfoDataFactory = pageInfoDataFactory;
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

			StartPageInfo payload = await Map( quiz );

			return new CommandResult<StartPageInfo> {
				IsSuccess = true,
				Message = string.Empty,
				Payload = payload
			};

		}

		public async Task<StartPageInfo> Map( Quiz quiz ) {

			var payload = new StartPageInfo();
			payload.UId = quiz.UId;
			payload.IsStartPageEnabled = quiz.IsStartPageEnabled;

			if( !quiz.IsStartPageEnabled ) {
				return payload;
			}

			payload.Name = quiz.Name;
			payload.Description = quiz.Description;

			if( quiz.IsTotalAttemptsEnabled ) {
				payload.TotalAttempts = int.MaxValue; //TODO:
			}

			if( quiz.IsTotalQuestionsEnabled ) {
				ImmutableArray<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( quiz.UId );
				ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( quiz.UId );

				List<Question> questions = _mapper.Map<List<Question>>( questionDtos );
				List<Group> groups = _mapper.Map<List<Group>>( groupDtos );
				List<PageInfo> pages = _pageInfoDataFactory.Create( quiz, groups, questions );

				payload.TotalQuestions = pages.SelectMany( x => x.Questions ).Count();
			}

			if( quiz.IsPassingScoreEnabled ) {
				payload.PassingScore = 80; //TODO:
			}

			if( quiz.IsTimeLimitEnabled ) {
				payload.TimeLimit = 60; //TODO:
			}

			return payload;
		}

	}
}
