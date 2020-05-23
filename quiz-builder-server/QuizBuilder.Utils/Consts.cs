using System.Text.Json;

namespace QuizBuilder.Utils {

	public static class Consts {

		public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions {
			PropertyNameCaseInsensitive = true,
			IgnoreNullValues = true
		};


	}
}
