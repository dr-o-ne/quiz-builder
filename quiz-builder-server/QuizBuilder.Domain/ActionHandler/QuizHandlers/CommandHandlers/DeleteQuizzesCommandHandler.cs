using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Domain.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizzesCommandHandler : ICommandHandler<DeleteQuizzesCommand, CommandResult> {

		private readonly IQuizDataProvider _quizDataProvider;

		public DeleteQuizzesCommandHandler( IQuizDataProvider quizDataProvider ) {
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizzesCommand command ) {
			//TODO: delete questions
			//TODO: delete groups
			await _quizDataProvider.Delete( command.UIds.ToList() );
			return new CommandResult( success: true, message: string.Empty );
		}

	}

}
