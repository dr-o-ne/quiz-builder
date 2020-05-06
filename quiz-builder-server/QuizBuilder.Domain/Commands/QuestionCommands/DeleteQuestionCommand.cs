using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class DeleteQuestionCommand : ICommand<CommandResult> {
		public Guid Id { get; set; }
	}
}
