using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {

	public sealed class LoginViewModel {

		[JsonPropertyName( "username" )]
		public string Username { get; set; }

		[JsonPropertyName( "token" )]
		public string Token { get; set; }

	}

}
