using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public abstract class Choice : IValidatable, IOrdered {

		public string Text { get; set; }

		public int Order { get; set; }

		public string Feedback { get; set; }

		public virtual bool IsValid() => !string.IsNullOrWhiteSpace( Text );
	}

}
