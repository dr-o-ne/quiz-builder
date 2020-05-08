using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries.QuestionQueries {
	public class GetAllQuestionQuery : IQuery<GetAllQuestionsDto> {
	}

	public class GetQuestionsByGroupIdQuery : IQuery<GetQuestionsByGroupIdDto> {
		public Guid GroupId { get; set; }
	}
}
