namespace QuizBuilder.Data.Dto {

	public sealed class QuestionDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public string Name { get; set; }

		public int TypeId { get; set; }

		public string Text { get; set; }

		public string Settings { get; set; }
	}

}
