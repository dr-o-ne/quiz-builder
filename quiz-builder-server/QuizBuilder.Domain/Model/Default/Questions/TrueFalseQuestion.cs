using QuizBuilder.Domain.Model.Default.Choices;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public override bool IsValid() {

			return base.IsValid() && TrueChoice.IsCorrect != FalseChoice.IsCorrect;

		}

		public override Enums.QuestionType Type => Enums.QuestionType.TrueFalse;
	}
}
