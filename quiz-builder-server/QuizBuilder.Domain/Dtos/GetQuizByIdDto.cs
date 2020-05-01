using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {
	public class GetQuizByIdDto {
		public QuizViewModel Quiz { get; }

		public GetQuizByIdDto( QuizViewModel quiz ) {
			Quiz = quiz;
		}
	}
}
