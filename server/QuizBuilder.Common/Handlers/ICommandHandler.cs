using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Handlers {

	public interface ICommandHandler<in TCommand, TResult>
		where TCommand : ICommand<TResult>
		where TResult : ICommandResult {
		Task<TResult> HandleAsync( TCommand command );
	}

	public interface ICommandHandler<in TCommand> where TCommand : ICommand {

		Task HandleAsync( TCommand command );

	}

}
