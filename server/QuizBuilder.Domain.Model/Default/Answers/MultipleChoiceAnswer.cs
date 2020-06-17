namespace QuizBuilder.Domain.Model.Default.Answers {

	public sealed class MultipleChoiceAnswer : Answer {

		public long ChoiceId { get; }

		public MultipleChoiceAnswer( string questionUId, long choiceId ) : base( questionUId ) {
			ChoiceId = choiceId;
		}

		public override bool IsValid() =>
			base.IsValid() &&
			ChoiceId >= 0;
	}

}
