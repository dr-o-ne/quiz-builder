using QuizBuilder.Domain.Model.Default.Choices;
using static QuizBuilder.Domain.Model.Enums;
using static QuizBuilder.Domain.Model.Enums.ChoicesDisplayType;
using static QuizBuilder.Domain.Model.Enums.ChoicesEnumerationType;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public ChoicesDisplayType ChoicesDisplayType { get; set; } = Vertical;

		public ChoicesEnumerationType ChoicesEnumerationType { get; set; } = NoEnumeration;

		public override QuestionType Type { get => TrueFalse; }

		public TrueFalseQuestion WithoutCorrectChoices() {
			TrueChoice.IsCorrect = null;
			FalseChoice.IsCorrect = null;
			return this;
		}

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			TrueChoice.IsCorrect != null &&
			FalseChoice.IsCorrect != null &&
			TrueChoice.IsCorrect != FalseChoice.IsCorrect;
	}
}
