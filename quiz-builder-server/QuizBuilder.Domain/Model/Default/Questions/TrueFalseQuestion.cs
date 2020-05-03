using QuizBuilder.Domain.Model.Default.Choices;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public Enums.ChoicesDisplayType ChoicesDisplayType { get; set; } = Enums.ChoicesDisplayType.Vertical;

		public Enums.ChoicesEnumerationType ChoicesEnumerationType { get; set; } = Enums.ChoicesEnumerationType.NoEnumeration;

		public override Enums.QuestionType Type { get => Enums.QuestionType.TrueFalse; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			TrueChoice.IsCorrect != FalseChoice.IsCorrect;
	}
}
