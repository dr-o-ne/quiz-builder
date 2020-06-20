namespace QuizBuilder.Domain.Model.Default.Answers {

	public sealed class LongAnswerAnswer : Answer {

		public string Text { get; }

		public LongAnswerAnswer( string questionUId, string text ) : base( questionUId ) {
			Text = text;
		}

		public override bool IsValid() =>
			base.IsValid() &&
			!string.IsNullOrWhiteSpace( Text );
	}
}
