using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {

	public sealed class QuestionViewModel {

		public string Id { get; set; }
		public string GroupId { get; set; }
		public string Name { get; set; }
		public QuizItemType Type { get; set; }
		public string Text { get; set; }
		public decimal? Points { get; set; }
		public string Feedback { get; set; }
		public string CorrectFeedback { get; set; }
		public string IncorrectFeedback { get; set; }
		public string Settings { get; set; }
		public string Choices { get; set; }
		public bool IsRequired { get; set; }

	}
}
