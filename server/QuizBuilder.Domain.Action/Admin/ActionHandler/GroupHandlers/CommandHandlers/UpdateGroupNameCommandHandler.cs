using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class UpdateGroupNameCommandHandler : ICommandHandler<UpdateGroupNameCommand, CommandResult> {

		private readonly IGroupDataProvider _groupDataProvider;

		public UpdateGroupNameCommandHandler( IGroupDataProvider groupDataProvider ) {
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateGroupNameCommand command ) {

			GroupDto groupDto = await _groupDataProvider.Get( command.UId );
			if( !string.Equals( groupDto.Name, command.Name, StringComparison.Ordinal ) ) {
				groupDto.Name = command.Name;
				await _groupDataProvider.Update( groupDto );
			}

			return new CommandResult( success: true, message: string.Empty );
		}

	}
}
