using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.ActionHandlers {

	public interface ICommandHandler<in TCommand, TResult>
		where TCommand : ICommand<TResult>
		where TResult : ICommandResult {
		Task<TResult> HandleAsync( TCommand command );
	}

	public interface ICommandHandler<in TCommand> where TCommand : ICommand {

		Task HandleAsync( TCommand command );

	}

}
