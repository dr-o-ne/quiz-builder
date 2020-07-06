using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {
	public sealed class MoveQuestionCommandHandler : ICommandHandler<MoveQuestionCommand, CommandResult> {
		private readonly IGroupDataProvider _groupDataProvider;

		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public MoveQuestionCommandHandler(
			IQuestionDataProvider questionDataProvider,
			IStructureDataProvider structureDataProvider,
			IGroupDataProvider groupDataProvider ) {
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<CommandResult> HandleAsync( MoveQuestionCommand command ) {
			GroupDto groupDto = await _groupDataProvider.Get( command.NewGroupUId );
			if( groupDto == null )
				return new QuestionCommandResult {Success = false};

			QuestionDto questionDto = await _questionDataProvider.Get( command.QuestionUId );
			if( questionDto == null )
				return new QuestionCommandResult { Success = false };

			await _structureDataProvider.UpdateGroupQuizItemRelationship( groupDto.Id, command.QuestionUId );

			return new CommandResult( true, string.Empty );
		}
	}
}
