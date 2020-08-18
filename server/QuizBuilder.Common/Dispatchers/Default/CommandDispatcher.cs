using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Dispatchers.Default {

	internal sealed class CommandDispatcher : ICommandDispatcher {

		private readonly IServiceProvider _serviceProvider;

		public CommandDispatcher( IServiceProvider serviceProvider ) {
			_serviceProvider = serviceProvider;
		}

		public async Task SendAsync<TCommand>( TCommand command ) where TCommand : ICommand {
			await _serviceProvider.GetService<ICommandHandler<TCommand>>().HandleAsync( command );
		}

		public async Task<TResult> SendAsync<TResult>( ICommand<TResult> command ) where TResult : ICommandResult {
			Type handlerType = typeof(ICommandHandler<,>).MakeGenericType( command.GetType(), typeof(TResult) );

			dynamic handler = _serviceProvider.GetService( handlerType );

			return await handler.HandleAsync( (dynamic)command );
		}
	}
}
