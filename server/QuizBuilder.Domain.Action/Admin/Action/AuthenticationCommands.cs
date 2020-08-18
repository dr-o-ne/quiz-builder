using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class RegisterUserCommand : ICommand<CommandResult> {

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

	public sealed class LoginUserCommand : ICommand<CommandResult<LoginInfo>> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

}
