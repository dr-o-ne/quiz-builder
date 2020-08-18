using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.Dispatchers {

	internal interface ICommandDispatcher {

		Task SendAsync<TCommand>( TCommand command ) where TCommand : ICommand;

		Task<TResult> SendAsync<TResult>( ICommand<TResult> command ) where TResult : ICommandResult;
	}
}
