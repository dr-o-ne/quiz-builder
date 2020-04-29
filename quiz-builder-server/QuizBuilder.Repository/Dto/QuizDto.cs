namespace QuizBuilder.Repository.Dto {
	public sealed class QuizDto {
		public long Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; } = "In design";
	}
}
