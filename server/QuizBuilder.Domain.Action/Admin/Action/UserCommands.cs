using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class SignUpCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

	public sealed class AuthenticateUserCommand : ICommand<AuthenticateUserResult> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

}
