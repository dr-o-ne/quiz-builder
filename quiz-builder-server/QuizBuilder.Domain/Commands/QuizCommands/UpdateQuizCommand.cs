using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Commands.QuizCommands {
	public sealed class UpdateQuizCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName("Id")]
		public string UId { get; set; }

		[Required]
		public string Name { get; set; }

		public bool IsVisible { get; set; }

	}
}
