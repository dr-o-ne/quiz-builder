using QuizBuilder.Common.Types;
using QuizBuilder.Domain.ActionResult.Dto;

namespace QuizBuilder.Domain.Action {

	public sealed class GetAllQuizzesQuery : IQuery<GetAllQuizzesDto> {
	}

	public sealed class GetQuizByIdQuery : IQuery<GetQuizByIdDto> {

		public string UId { get; set; }

	}

}
