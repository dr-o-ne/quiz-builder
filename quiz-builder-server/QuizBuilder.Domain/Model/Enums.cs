namespace QuizBuilder.Domain.Model {

	public static class Enums {

		public enum QuestionType {

			None = 0,
			TrueFalse = 1,
			MultiChoice = 2,
			FillInTheBlanks = 3,
			MultiSelect = 4,
			LongAnswer = 5
		}

		public enum TextEvaluationType {
			None = 0,
			CaseSensitive,
			CaseInsensitive,
			RegularExpression
		}

		public enum QuestionGradingType {
			None = 0,
			AllOrNothing = 1,
			RightMinusWrong = 2,
			CorrectAnswers = 3
		}

		public enum ChoicesDisplayType {
			None = 0,
			Horizontal = 1,
			Vertical = 2,
			Dropdown = 3
		}

		public enum ChoicesEnumerationType {
			None = 0,
			one_two_three = 1,
			i_ii_iii = 2,
			I_II_III = 3,
			a_b_c = 4,
			A_B_C = 5,
			NoEnumeration = 6
		}

	}
}
