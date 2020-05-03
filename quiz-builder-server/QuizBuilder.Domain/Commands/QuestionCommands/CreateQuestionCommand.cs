using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class CreateQuestionCommand : ICommand<CommandResult> {
		public string Name { get; set; }
		public long GroupId { get; set; }
		public Enums.QuestionType Type { get; set; }
		public string Text { get; set; }
	}
}
