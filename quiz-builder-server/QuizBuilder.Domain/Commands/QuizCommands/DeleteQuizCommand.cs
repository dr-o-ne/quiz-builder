using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {

	public sealed class DeleteQuizCommand : ICommand<CommandResult> {
		public Guid Id { get; set; }
	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult> {
		public Guid[] Ids { get; set; }
	}

}
