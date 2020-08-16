using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Action.Admin.ActionResult {

	public sealed class LoginInfo {

		[JsonPropertyName( "username" )]
		public string Username { get; set; }

		[JsonPropertyName( "token" )]
		public string Token { get; set; }

	}

}
