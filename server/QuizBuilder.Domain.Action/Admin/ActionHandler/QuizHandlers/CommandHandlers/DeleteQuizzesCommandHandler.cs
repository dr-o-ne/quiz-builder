using System.Threading.Tasks;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizzesCommandHandler : ICommandHandler<DeleteQuizzesCommand, CommandResult> {

		private readonly IDispatcher _dispatcher;

		public DeleteQuizzesCommandHandler( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizzesCommand command ) {

			foreach( string uid in command.UIds ) {
				await _dispatcher.SendAsync( new DeleteQuizCommand {UId = uid} );
			}

			return new CommandResult( success: true, message: string.Empty );
		}

	}

}
