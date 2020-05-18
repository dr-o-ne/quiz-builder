using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.ActionResult.ViewModel {
	public sealed class QuestionViewModel {
		public string Id { get; set; }
		public string Name { get; set; }
		public Enums.QuestionType Type { get; set; }
		public long GroupId { get; set; } = 1;
		public string Text { get; set; }
		public string Feedback { get; set; }
		public string CorrectFeedback { get; set; }
		public string IncorrectFeedback { get; set; }
		public string Settings { get; set; }
		public string Choices { get; set; }
	}
}
