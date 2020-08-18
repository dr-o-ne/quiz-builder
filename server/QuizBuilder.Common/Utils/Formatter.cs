namespace QuizBuilder.Common.Utils {

	public static class Formatter {

		public static string NormalizeEmail( string email ) => email?.Trim().ToLower();

	}

}
