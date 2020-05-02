using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries.QuestionQueries {
	public class GetAllQuestionQuery : IQuery<GetAllQuestionsDto> {
	}

	public class GetQuestionsByGroupIdQuery : IQuery<GetQuestionsByGroupIdDto> {
		public long GroupId { get; set; }
	}
}
