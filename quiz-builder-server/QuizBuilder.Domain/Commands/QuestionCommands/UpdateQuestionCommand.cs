using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class UpdateQuestionCommand : ICommand<CommandResult> {
		public long Id { get; set; }
		public string Name { get; set; }
		public long GroupId { get; set; }
		public Enums.QuestionType Type { get; set; }
		public string Text { get; set; }
	}
}
