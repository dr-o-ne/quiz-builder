using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, CommandResult> {

		private readonly IGroupDataProvider _groupDataProvider;

		public UpdateGroupCommandHandler( IGroupDataProvider groupDataProvider ) {
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateGroupCommand command ) {

			GroupDto groupDto = await _groupDataProvider.Get( command.UId );
			if( groupDto == null )
				return CommandResult.Fail();



			throw new System.NotImplementedException();

		}
	}
}
