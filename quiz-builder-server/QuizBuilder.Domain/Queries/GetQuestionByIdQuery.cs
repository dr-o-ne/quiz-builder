using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries
{
	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public long Id { get; set; }
	}
}