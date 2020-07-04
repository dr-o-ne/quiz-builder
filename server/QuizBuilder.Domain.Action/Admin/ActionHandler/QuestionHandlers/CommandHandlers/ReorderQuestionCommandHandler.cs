using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class ReorderQuestionCommandHandler : ICommandHandler<ReorderQuestionsCommand, CommandResult> {

		public Task<CommandResult> HandleAsync( ReorderQuestionsCommand command ) {
			throw new System.NotImplementedException();
		}

	}

}
