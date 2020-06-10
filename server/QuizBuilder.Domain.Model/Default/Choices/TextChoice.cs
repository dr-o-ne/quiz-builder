using static QuizBuilder.Domain.Model.Default.Enums.TextEvaluationType;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class TextChoice : Choice {

		public Enums.TextEvaluationType TextEvaluationType { get; set; }

		public override bool IsValid() =>
			base.IsValid() && TextEvaluationType != None;
	}

}
