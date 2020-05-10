using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {

	public sealed class CreateQuizCommand : ICommand<CommandResult> {

		[Required]
		public string Name { get; set; }

		public bool IsVisible { get; set; }

	}

}
