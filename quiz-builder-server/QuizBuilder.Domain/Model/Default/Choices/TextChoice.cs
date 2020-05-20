using static QuizBuilder.Domain.Model.Enums;
using static QuizBuilder.Domain.Model.Enums.TextEvaluationType;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class TextChoice : Choice {

		public TextEvaluationType TextEvaluationType { get; set; }

		public override bool IsValid() =>
			base.IsValid() && TextEvaluationType != None;
	}

}
