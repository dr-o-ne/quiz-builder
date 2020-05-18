using QuizBuilder.Common.Types;
using QuizBuilder.Domain.ActionResult;

namespace QuizBuilder.Domain.Action {

	public sealed class GetAllQuizzesQuery : IQuery<QuizzesQueryResult> {
	}

	public sealed class GetQuizByIdQuery : IQuery<QuizQueryResult> {

		public string UId { get; set; }

	}

}
