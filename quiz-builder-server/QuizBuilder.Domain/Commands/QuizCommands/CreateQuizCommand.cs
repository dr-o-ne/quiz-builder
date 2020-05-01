using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {
	public class CreateQuizCommand : ICommand<CommandResult> {
		[Required]
		[MaxLength( 100 )]
		public string Name { get; set; }
		public string Status { get; set; }
		public bool IsVisible { get; set; }
	}
}
