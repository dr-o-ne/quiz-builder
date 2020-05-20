using static QuizBuilder.Domain.Model.Enums;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class TextChoice : Choice {

		public TextEvaluationType TextEvaluationType { get; set; }

		public override bool IsValid() =>
			base.IsValid() && TextEvaluationType != TextEvaluationType.None;
	}

}
