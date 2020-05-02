namespace QuizBuilder.Domain.Model.View {
	public class QuestionViewModel {
		public long Id { get; set; }
		public Enums.QuestionType Type { get; set; }
		public long GroupId { get; set; } = 1;
		public string Name { get; set; }
		public string Text { get; set; }
	}
}
