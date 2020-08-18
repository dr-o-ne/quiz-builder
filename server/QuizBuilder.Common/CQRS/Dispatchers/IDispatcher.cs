using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.Dispatchers {

	public interface IDispatcher {

		Task SendAsync<TCommand>( TCommand command ) where TCommand : ICommand;

		Task<TResult> SendAsync<TResult>( ICommand<TResult> command ) where TResult : ICommandResult;

		Task<TResult> QueryAsync<TResult>( IQuery<TResult> query );

	}

}
