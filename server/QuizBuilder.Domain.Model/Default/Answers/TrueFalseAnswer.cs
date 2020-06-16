namespace QuizBuilder.Domain.Model.Default.Answers {

	public sealed class TrueFalseAnswer : Answer {

		public long ChoiceId { get; }

		public TrueFalseAnswer(string questionUId, long choiceId ) : base(questionUId) {
			ChoiceId = choiceId;
		}

		public override bool IsValid() => ChoiceId == 0 || ChoiceId == 1;

	}

}
