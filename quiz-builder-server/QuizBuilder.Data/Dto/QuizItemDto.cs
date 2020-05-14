namespace QuizBuilder.Data.Dto {

	public sealed class QuizItemDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public long? ParentQuizItemId { get; set; }

		public long? QuestionId { get; set; }

		public int TypeId { get; set; }

	}

}
