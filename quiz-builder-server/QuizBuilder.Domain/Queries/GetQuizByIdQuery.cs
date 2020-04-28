using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Queries
{
	public class GetQuizByIdQuery : IQuery<GetQuizByIdDto> {
		public long Id { get; set; }
	}
}