using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuestionCommands {

	public sealed class DeleteQuestionCommand : ICommand<CommandResult> {

		public string UId { get; set; }

	}

}
