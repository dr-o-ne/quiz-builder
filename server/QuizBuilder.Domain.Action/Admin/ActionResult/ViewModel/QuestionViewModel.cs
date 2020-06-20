using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {
	public sealed class QuestionViewModel {
		public string Id { get; set; }
		public string Name { get; set; }
		public Enums.QuestionType Type { get; set; }
		public long GroupId { get; set; }
		public string Text { get; set; }
		public decimal? Points { get; set; }
		public string Feedback { get; set; }
		public string CorrectFeedback { get; set; }
		public string IncorrectFeedback { get; set; }
		public string Settings { get; set; }
		public string Choices { get; set; }
	}
}
