using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Domain.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.ActionHandler.QuizAttemptHandlers.CommandHandlers {

	public sealed class StartQuizAttemptCommandHandler : ICommandHandler<StartQuizAttemptCommand, QuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;

		public StartQuizAttemptCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuestionDataProvider questionDataProvider,
			IQuizAttemptDataProvider attemptDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_questionDataProvider = questionDataProvider;
			_attemptDataProvider = attemptDataProvider;
		}

		public async Task<QuizAttemptCommandResult> HandleAsync( StartQuizAttemptCommand command ) {

			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( command.QuizUId );

			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			questions = questions.Select( x => x.ToQuestionWithoutCorrectChoices() );

			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			var quizAttempt = new QuizAttempt {
				UId = _uIdService.GetUId(),
				QuizUId = command.QuizUId,
				StartDate = DateTime.UtcNow,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			};
			var quizAttemptDto = _mapper.Map<QuizAttempt, AttemptDto>( quizAttempt );
			await _attemptDataProvider.Add( quizAttemptDto );

			//TODO: problem: how to match attempts with modified quiz? Serialize and save questions?

			return new QuizAttemptCommandResult {
				Success = true,
				QuizAttempt = new QuizAttemptViewModel {
					Id = quizAttempt.UId,
					QuizId = quizAttempt.QuizUId
				},
				Questions = questionViewModels.ToImmutableList()
			};
		}

	}
}
