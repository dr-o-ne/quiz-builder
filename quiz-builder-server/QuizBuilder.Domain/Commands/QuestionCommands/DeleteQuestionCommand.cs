using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class DeleteQuestionCommand : ICommand<CommandResult> {
		public long Id { get; set; }
	}
}
