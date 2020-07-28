using static QuizBuilder.Domain.Model.Default.Enums;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class LongAnswerQuestion : Question {

		public override QuizItemType Type { get => LongAnswer; }

		public override bool IsValid() => !string.IsNullOrWhiteSpace( Text );

	}
}
