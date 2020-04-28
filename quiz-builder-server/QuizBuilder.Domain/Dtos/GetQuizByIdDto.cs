using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Dtos {
	public class GetQuizByIdDto {
		public QuizDto Quiz { get; }

		public GetQuizByIdDto( QuizDto quiz ) {
			Quiz = quiz;
		}
	}
}
