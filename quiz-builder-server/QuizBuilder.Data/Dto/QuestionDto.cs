namespace QuizBuilder.Data.Dto {

	public sealed class QuestionDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public string Name { get; set; }

		public int QuestionTypeId { get; set; }

		public string QuestionText { get; set; }

		public string Settings { get; set; }
	}

}
