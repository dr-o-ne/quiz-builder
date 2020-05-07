using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries.QuestionQueries {

	public sealed class GetQuestionsByQuizIdQuery : IQuery<GetAllQuestionsDto> {

		public long QuizId { get; set; }

	}

}
