using QuizBuilder.Model.Model.Default.Choices;

namespace QuizBuilder.Model.Model.Default.Questions {

	public sealed class TrueFalseQuestion : Question {

		public BinaryChoice TrueChoice { get; set; } = new BinaryChoice();

		public BinaryChoice FalseChoice { get; set; } = new BinaryChoice();

		public override bool IsValid() {

			return base.IsValid() && TrueChoice.IsCorrect != FalseChoice.IsCorrect;

		}
	}
}
