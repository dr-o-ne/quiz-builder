namespace QuizBuilder.Data.Dto {

	public sealed class GroupDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public long ParentId { get; set; }

		public string Name { get; set; }

		public int SortOrder { get; set; }

	}

}
