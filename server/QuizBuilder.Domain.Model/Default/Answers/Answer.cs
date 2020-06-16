using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Answers {

	public abstract class Answer : IValidatable {

		public string QuestionUId { get; set; }

		protected Answer()
		{
		}

		protected Answer( string questionUId ) {
			QuestionUId = questionUId;
		}

		public virtual bool IsValid() => !string.IsNullOrWhiteSpace( QuestionUId );
	}

}
