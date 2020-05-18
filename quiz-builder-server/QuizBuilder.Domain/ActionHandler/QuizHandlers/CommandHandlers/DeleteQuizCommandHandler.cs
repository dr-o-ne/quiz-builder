using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Domain.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, CommandResult> {

		private readonly IQuizDataProvider _quizDataProvider;

		public DeleteQuizCommandHandler( IQuizDataProvider quizDataProvider ) {
			_quizDataProvider = quizDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizCommand command ) {
			await _quizDataProvider.Delete( command.UId );

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}
