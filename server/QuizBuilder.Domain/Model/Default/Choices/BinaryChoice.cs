namespace QuizBuilder.Domain.Model.Default.Choices {

	public sealed class BinaryChoice : Choice {

		public bool? IsCorrect { get; set; }

		public override bool IsValid() =>
			base.IsValid() && IsCorrect != null;

	}

}
