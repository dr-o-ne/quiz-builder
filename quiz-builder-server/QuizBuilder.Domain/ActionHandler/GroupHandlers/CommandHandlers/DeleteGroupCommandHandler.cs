using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Domain.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, CommandResult> {

		public Task<CommandResult> HandleAsync( DeleteGroupCommand command ) {
			throw new NotImplementedException(); //TODO:
		}

	}

}
