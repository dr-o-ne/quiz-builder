using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Domain.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, CommandResult> {

		public Task<CommandResult> HandleAsync( UpdateGroupCommand command ) {
			throw new NotImplementedException(); //TODO:
		}

	}
}
