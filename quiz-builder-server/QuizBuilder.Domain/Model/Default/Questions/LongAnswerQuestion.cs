namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class LongAnswerQuestion : Question {

		public override Enums.QuestionType Type { get => Enums.QuestionType.LongAnswer; }

		public override bool IsValid() => !string.IsNullOrWhiteSpace( Text );

		public string AnswerText { get; set; }
	}
}
