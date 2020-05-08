using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries.QuizQueries
{
	public class GetQuizByIdQuery : IQuery<GetQuizByIdDto> {
		public Guid Id { get; set; }
	}
}
