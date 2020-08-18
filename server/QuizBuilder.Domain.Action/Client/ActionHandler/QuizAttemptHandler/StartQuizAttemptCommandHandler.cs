using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Action.Client.Services;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Appearance;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class StartQuizAttemptCommandHandler : ICommandHandler<StartQuizAttemptCommand, StartQuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IPageInfoDataFactory _pageInfoDataFactory;

		public StartQuizAttemptCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IQuestionDataProvider questionDataProvider,
			IQuizAttemptDataProvider attemptDataProvider,
			IGroupDataProvider groupDataProvider,
			IPageInfoDataFactory pageInfoDataFactory ) {

			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_attemptDataProvider = attemptDataProvider;
			_groupDataProvider = groupDataProvider;
			_pageInfoDataFactory = pageInfoDataFactory;
		}

		public async Task<StartQuizAttemptCommandResult> HandleAsync( StartQuizAttemptCommand command ) {

			string quizUId = command.QuizUId;

			if( string.IsNullOrWhiteSpace( quizUId ) )
				return new StartQuizAttemptCommandResult { IsSuccess = false };

			QuizDto quizDto = await _quizDataProvider.Get( quizUId );

			if( quizDto == null )
				return new StartQuizAttemptCommandResult { IsSuccess = false };

			//TODO: load from DB
			var appearance = new Appearance {
				ShowQuizName = true,
				HeaderColor = "#1a202e",
				MainColor = "#f5f5f8",
				CardColor = "#fff",
				FooterColor = "#fff"
			};

			Quiz quiz = _mapper.Map<Quiz>( quizDto );

			if( !quiz.IsValid() )
				return new StartQuizAttemptCommandResult {IsSuccess = false};

			if( !quiz.IsAvailable() )
				return new StartQuizAttemptCommandResult {IsSuccess = false};

			ImmutableArray<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( command.QuizUId );
			ImmutableArray<GroupDto> groupDtos = await _groupDataProvider.GetByQuiz( command.QuizUId );

			List<Question> questions = _mapper.Map<List<Question>>( questionDtos );
			List<Group> groups = _mapper.Map<List<Group>>( groupDtos );

			QuizAttempt quizAttempt = await CreateQuizAttempt( quizUId );

			return new StartQuizAttemptCommandResult {
				IsSuccess = true,
				Message = string.Empty,
				Payload = MapPayload( quizAttempt.UId, quiz, groups, questions, appearance )
			};

		}

		private async Task<QuizAttempt> CreateQuizAttempt( string quizUId ) {

			var quizAttempt = new QuizAttempt {
				UId = _uIdService.GetUId(),
				QuizUId = quizUId,
				StartDate = DateTime.UtcNow,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			};

			var quizAttemptDto = _mapper.Map<AttemptDto>( quizAttempt );
			await _attemptDataProvider.Add( quizAttemptDto );

			return quizAttempt;
		}

		private QuizAttemptInfo MapPayload( string uid, Quiz quiz, List<Group> groups, List<Question> questions, Appearance appearance ) {

			var result = new QuizAttemptInfo {
				UId = uid,
				Name = appearance.ShowQuizName ? quiz.Name : string.Empty,
				SettingsInfo = _mapper.Map<SettingsInfo>( quiz ),
				AppearanceInfo = _mapper.Map<AppearanceInfo>( appearance ),
				Pages = _pageInfoDataFactory.Create( quiz, groups, questions )
			};

			return result;
		}

	}

}
