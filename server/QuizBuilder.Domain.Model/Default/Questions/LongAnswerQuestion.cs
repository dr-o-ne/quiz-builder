using static QuizBuilder.Domain.Model.Default.Enums;
using static QuizBuilder.Domain.Model.Default.Enums.QuestionType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class LongAnswerQuestion : Question {

		public override QuestionType Type { get => LongAnswer; }

		public override Question NullifyChoices() {
			return this;
		}

		public override bool IsValid() => !string.IsNullOrWhiteSpace( Text );

	}
}
