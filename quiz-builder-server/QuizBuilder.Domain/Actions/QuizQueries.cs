using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Actions {

	public sealed class GetAllQuizzesQuery : IQuery<GetAllQuizzesDto> {
	}

	public sealed class GetQuizByIdQuery : IQuery<GetQuizByIdDto> {

		public string UId { get; set; }

	}

}
