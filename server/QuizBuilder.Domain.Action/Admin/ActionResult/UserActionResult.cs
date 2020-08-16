using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class AuthenticateUserResult : CommandResult {

		public string Username { get; set; }

		public string Token { get; set; }

	}

}
