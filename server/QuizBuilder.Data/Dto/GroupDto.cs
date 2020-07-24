namespace QuizBuilder.Data.Dto {

	public sealed class GroupDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public string Name { get; set; }

		public string Settings { get; set; }

		public bool IsEnabled { get; set; }

		public int SortOrder { get; set; }

	}

}
