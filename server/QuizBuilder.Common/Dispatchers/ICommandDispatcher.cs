using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Dispatchers {

	internal interface ICommandDispatcher {

		Task SendAsync<TCommand>( TCommand command ) where TCommand : ICommand;

		Task<TResult> SendAsync<TResult>( ICommand<TResult> command ) where TResult : ICommandResult;
	}
}
