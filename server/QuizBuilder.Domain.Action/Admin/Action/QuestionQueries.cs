using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetQuestionByIdQuery : IQuery<QuestionQueryResult> {
		public string UId { get; set; }
	}
}
