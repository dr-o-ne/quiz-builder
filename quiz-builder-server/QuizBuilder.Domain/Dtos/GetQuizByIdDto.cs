using QuizBuilder.Domain.Model.View;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Dtos {
	public class GetQuizByIdDto {
		public QuizViewModel Quiz { get; }

		public GetQuizByIdDto( QuizViewModel quiz ) {
			Quiz = quiz;
		}
	}
}
