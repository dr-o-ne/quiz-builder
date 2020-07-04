using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.GroupHandlers.CommandHandlers {

	public sealed class ReorderGroupCommandHandler : ICommandHandler<ReorderGroupsCommand, CommandResult> {

		private readonly IGroupDataProvider _groupDataProvider;

		public ReorderGroupCommandHandler( IGroupDataProvider groupDataProvider ) {
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( ReorderGroupsCommand command ) {

			var groupDtos = ( await _groupDataProvider.GetByQuiz( command.QuizUId ) ).ToList();

			for( int i = 0; i < command.GroupUIds.Length; i++ ) {
				var groupDto = groupDtos.Single( x => x.UId == command.GroupUIds[i] );
				groupDto.SortOrder = i;
				await _groupDataProvider.Update( groupDto );
			}

			return new CommandResult( success: true, message: string.Empty );
		}

	}

}
