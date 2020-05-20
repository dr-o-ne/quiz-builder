using QuizBuilder.Domain.Model.Default.Choices;
using static QuizBuilder.Domain.Model.Enums;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public ChoicesDisplayType ChoicesDisplayType { get; set; } = ChoicesDisplayType.Vertical;

		public ChoicesEnumerationType ChoicesEnumerationType { get; set; } = ChoicesEnumerationType.NoEnumeration;

		public override QuestionType Type { get => QuestionType.TrueFalse; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			TrueChoice.IsCorrect != FalseChoice.IsCorrect;
	}
}
