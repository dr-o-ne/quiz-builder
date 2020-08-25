using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class RegisterUserCommand : ICommand<CommandResult<LoginViewModel>> {

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

	public sealed class LoginUserCommand : ICommand<CommandResult<LoginViewModel>> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

	public sealed class ForgotPasswordCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

	}

	public sealed class NewPasswordCommand : ICommand<CommandResult<LoginViewModel>> {

		[JsonPropertyName( "code" )]
		public string Code { get; set; }

		[Required]
		[JsonPropertyName( "email" )]
		public string Email { get; set; }

		[Required]
		[JsonPropertyName( "password" )]
		public string Password { get; set; }

	}

}
