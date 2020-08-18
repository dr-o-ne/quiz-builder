using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Dispatchers.Default {

	internal sealed class Dispatcher : IDispatcher {

		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;

		public Dispatcher( ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher ) {
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public Task SendAsync<TCommand>( TCommand command ) where TCommand : ICommand
			=> _commandDispatcher.SendAsync( command );

		public Task<TResult> SendAsync<TResult>( ICommand<TResult> command ) where TResult : ICommandResult
			=> _commandDispatcher.SendAsync( command );

		public Task<TResult> QueryAsync<TResult>( IQuery<TResult> query )
			=> _queryDispatcher.QueryAsync( query );

	}
}
