using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Domain.Action.Client.ActionHandler.QuizAttemptHandler {

	public sealed class EndQuizAttemptCommandHandler : ICommandHandler<EndQuizAttemptCommand, EndQuizAttemptCommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuizAttemptDataProvider _attemptDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;

		public EndQuizAttemptCommandHandler(
			IMapper mapper,
			IQuizDataProvider quizDataProvider,
			IQuizAttemptDataProvider attemptDataProvider,
			IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
			_attemptDataProvider = attemptDataProvider;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<EndQuizAttemptCommandResult> HandleAsync( EndQuizAttemptCommand command ) {

			return new EndQuizAttemptCommandResult();
		}

	}
}
