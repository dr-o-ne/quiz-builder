namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class TextChoice : Choice {

		public Enums.TextEvaluationType TextEvaluationType { get; set; }

		public override bool IsValid() =>
			base.IsValid() && TextEvaluationType != Enums.TextEvaluationType.None;
	}

}
