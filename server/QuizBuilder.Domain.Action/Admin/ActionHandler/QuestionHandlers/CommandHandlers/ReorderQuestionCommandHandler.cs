using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class ReorderQuestionCommandHandler : ICommandHandler<ReorderQuestionsCommand, CommandResult> {

		private readonly IQuestionDataProvider _questionDataProvider;

		public ReorderQuestionCommandHandler( IQuestionDataProvider questionDataProvider ) {
			_questionDataProvider = questionDataProvider;
		}

		public async Task<CommandResult> HandleAsync( ReorderQuestionsCommand command ) {

			List<QuestionDto> dtos = ( await _questionDataProvider.GetByGroup( command.GroupUId ) )
				.OrderBy( x=>x.SortOrder )
				.ToList();

			for( int i = 0; i < command.QuestionUIds.Length; i++ ) {
				QuestionDto dto = dtos.Single( x => x.UId == command.QuestionUIds[i] );
				dto.SortOrder = i;
				await _questionDataProvider.Update( dto );
			}

			return new CommandResult( true, string.Empty );
		}

	}

}
