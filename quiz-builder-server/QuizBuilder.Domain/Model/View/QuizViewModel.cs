namespace QuizBuilder.Domain.Model.View {
	public sealed class QuizViewModel {
		public string Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; } = "In develop";
		public bool IsVisible { get; set; }
	}
}
