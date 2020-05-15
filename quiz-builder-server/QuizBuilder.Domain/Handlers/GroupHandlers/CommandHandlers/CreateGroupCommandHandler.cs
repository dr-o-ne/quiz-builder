using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Actions;

namespace QuizBuilder.Domain.Handlers.GroupHandlers.CommandHandlers {

	public sealed class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, CommandResult> {

		public Task<CommandResult> HandleAsync( CreateGroupCommand command ) {
			throw new NotImplementedException(); //TODO:
		}

	}
}
