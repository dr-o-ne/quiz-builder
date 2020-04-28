namespace QuizBuilder.Domain.Dtos {

	public sealed class GetQuestionByIdDto {

		public long Id { get; }
		public string Name { get; }

		public GetQuestionByIdDto( long id, string name ) {
			Id = id;
			Name = name;
		}
	}
}
