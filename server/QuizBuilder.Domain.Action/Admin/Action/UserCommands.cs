using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateUserCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}
}
