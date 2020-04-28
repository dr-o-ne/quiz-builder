using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Choices {

	public abstract class Choice : IValidatable {

		public string Text { get; set; }

		public bool IsValid() => !string.IsNullOrWhiteSpace( Text );
	}

}
