namespace QuizBuilder.Domain.Model.View {
	public class QuizViewModel {
		public long Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; } = "In develop";
		public bool IsVisible { get; set; }
	}
}
