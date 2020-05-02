using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {
	public sealed class GetQuestionByIdDto {
		public QuestionViewModel Question { get; }

		public GetQuestionByIdDto( QuestionViewModel question ) {
			Question = question;
		}
	}
}
