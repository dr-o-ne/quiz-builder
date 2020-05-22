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
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.ActionHandler.QuizAttemptHandlers.CommandHandlers {

	public sealed class CreateQuizAttemptCommandHandler : ICommandHandler<CreateQuizAttemptCommand, QuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public CreateQuizAttemptCommandHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<QuizAttemptCommandResult> HandleAsync( CreateQuizAttemptCommand command ) {

			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( command.QuizUId );

			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			questions = questions.Select( x => x.ToQuestionWithoutCorrectChoices() );

			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			//TODO: add entity, set start time

			return new QuizAttemptCommandResult { Success = true, Questions = questionViewModels.ToImmutableList() };
		}

	}
}
