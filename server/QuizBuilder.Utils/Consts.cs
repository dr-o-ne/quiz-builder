using System.Text.Json;

namespace QuizBuilder.Utils {

	public static class Consts {

		public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions {
			PropertyNameCaseInsensitive = true,
			IgnoreNullValues = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		public static class Role {

			public const string Admin = "Admin";
			public const string Instructor = "Instructor";
			public const string User = "User";

		}

		public static string JwtSecret = "QB_SECRET" + "12333333333333333331111111111111111111111244444444444"; //TODO: move to config
	}
}
