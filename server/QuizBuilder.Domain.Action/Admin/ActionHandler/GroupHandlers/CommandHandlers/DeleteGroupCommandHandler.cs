using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {
	public sealed class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, CommandResult> {
		private readonly IGroupDataProvider _groupDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public DeleteGroupCommandHandler(
			IGroupDataProvider groupDataProvider,
			IStructureDataProvider structureDataProvider ) {
			_groupDataProvider = groupDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<CommandResult> HandleAsync( DeleteGroupCommand command ) {
			await _structureDataProvider.RemoveQuizItemRelationships( command.UId );
			await _structureDataProvider.DeleteQuizQuizItemRelationships( command.UId );
			int rowsAffected = await _groupDataProvider.Delete( command.UId );

			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
