using System;

namespace QuizBuilder.Domain.Dtos {

	public class GetQuizByIdDto {

		public long Id { get; }
		public string Name { get; }
		public DateTime CreatedDate { get; }
		public DateTime UpdatedDate { get; }

		public GetQuizByIdDto( long id, string name, DateTime createdDate, DateTime updatedDate ) {
			Id = id;
			Name = name;
			CreatedDate = createdDate;
			UpdatedDate = updatedDate;
		}
	}
}
