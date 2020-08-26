namespace QuizBuilder.Domain.Model.Default {

	public static class Enums {

		public enum QuizItemType {
			None = 0,
			Group = 1,
			TrueFalse = 2,
			MultiChoice = 3,
			FillInTheBlanks = 4,
			MultiSelect = 5,
			LongAnswer = 6,
			Empty = 7
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

		public enum PageSettings {
			None = 0,
			PagePerGroup = 1,
			PagePerQuiz = 2,
			PagePerQuestion = 3,
			Custom = 4
		}
	}
}
