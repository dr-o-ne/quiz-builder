using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {

	public sealed class DeleteQuizCommand : ICommand<CommandResult> {
		public long Id { get; set; }
	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult> {
		public long[] Ids { get; set; }
	}

}
