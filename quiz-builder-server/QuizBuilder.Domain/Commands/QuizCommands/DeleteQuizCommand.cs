using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {

	public sealed class DeleteQuizCommand : ICommand<CommandResult> {
		public string Id { get; set; }
	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult> {
		public string[] Ids { get; set; }
	}

}
