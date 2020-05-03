namespace QuizBuilder.Domain.Model {

	public static class Enums {

		public enum QuestionType {

			None = 0,
			TrueFalse = 1,
			MultiChoice = 2,
			FillInTheBlanks = 3

		}

		public enum TextEvaluationType {
			None = 0,
			CaseSensitive,
			CaseInsensitive,
			RegularExpression
		}

	}
}
