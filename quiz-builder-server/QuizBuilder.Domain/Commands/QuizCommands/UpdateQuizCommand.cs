using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands {
	public class UpdateQuizCommand : ICommand<CommandResult> {
		[Required]
		public long Id { get; set; }
		[Required]
		[MaxLength( 100 )]
		public string Name { get; set; }
		public string Status { get; set; }
	}
}
