using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands {
	public class DeleteQuizCommand : ICommand<CommandResult> {
		public long Id { get; set; }
	}
}
