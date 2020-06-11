using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class StartQuizAttemptCommandHandler : ICommandHandler<StartQuizAttemptCommand, StartQuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;

		public StartQuizAttemptCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IQuestionDataProvider questionDataProvider,
			IQuizAttemptDataProvider attemptDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_attemptDataProvider = attemptDataProvider;
		}

		public async Task<StartQuizAttemptCommandResult> HandleAsync( StartQuizAttemptCommand command ) {

			string quizUId = command.QuizUId;

			if( string.IsNullOrWhiteSpace( quizUId ) )
				return new StartQuizAttemptCommandResult { Success = false };

			QuizDto quizDto = await _quizDataProvider.Get( quizUId );

			if( quizDto == null )
				return new StartQuizAttemptCommandResult { Success = false };

			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( command.QuizUId );

			Quiz quiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );

			QuizAttempt quizAttempt = await CreateQuizAttempt( quizUId );

			return new StartQuizAttemptCommandResult {
				Success = true,
				Message = string.Empty,
				Payload = BuildPayload( quizAttempt.UId, quiz, questions )
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

			var quizAttemptDto = _mapper.Map<QuizAttempt, AttemptDto>( quizAttempt );
			await _attemptDataProvider.Add( quizAttemptDto );

			return quizAttempt;

		}

		private static AttemptInfo BuildPayload( string uid, Quiz quiz, IEnumerable<Question> questions ) {

			var group = new GroupAttemptInfo {
				UId = string.Empty,
				Name = string.Empty,
				Questions = questions.Select( x => new QuestionAttemptInfo {UId = x.UId, Text = x.Text} ).ToImmutableArray()
			};

			return new AttemptInfo {
				UId = uid,
				Quiz = new QuizAttemptInfo {
					UId = quiz.UId,
					Name = quiz.Name,
					Groups = new List<GroupAttemptInfo> { group }.ToImmutableArray()
				}
			};
		}
	}

}
