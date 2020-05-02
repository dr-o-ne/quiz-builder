using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class CreateQuestionCommand : ICommand<CommandResult> {

		public string Name { get; set; }

		public string QuestionText { get; set; }

		public string Settings { get; set; }

		public Enums.QuestionType QuestionType { get; set; }
}
}
