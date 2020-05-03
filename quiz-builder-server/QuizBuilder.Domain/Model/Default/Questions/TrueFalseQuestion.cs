using QuizBuilder.Domain.Model.Default.Choices;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public Enums.ChoicesDisplayType ChoicesDisplayType { get; set; }

		public Enums.ChoicesEnumerationType ChoicesEnumerationType { get; set; }

		public override Enums.QuestionType Type { get => Enums.QuestionType.TrueFalse; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			ChoicesEnumerationType != Enums.ChoicesEnumerationType.None &&
			ChoicesDisplayType != Enums.ChoicesDisplayType.None &&
			TrueChoice.IsCorrect != FalseChoice.IsCorrect;
	}
}
