using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model.Default.Choices {

	public abstract class Choice : IValidatable {

		public string Text { get; set; }

		public bool IsValid() => !string.IsNullOrWhiteSpace( Text );
	}

}
