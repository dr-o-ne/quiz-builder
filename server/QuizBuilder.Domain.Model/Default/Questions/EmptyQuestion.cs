using static QuizBuilder.Domain.Model.Default.Enums;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class EmptyQuestion : Question {

		public override bool IsValid() => true;

		public override QuizItemType Type => Empty;

	}

}
