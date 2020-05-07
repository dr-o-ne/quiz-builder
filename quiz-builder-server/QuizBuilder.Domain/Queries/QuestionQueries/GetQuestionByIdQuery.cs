using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries.QuestionQueries
{
	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public Guid Id { get; set; }
	}
}
