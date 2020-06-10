using System.Collections.Immutable;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.Action {
	public sealed class GetQuestionsByParentQuery : IQuery<ImmutableList<QuestionViewModel>> {
		public string QuizUId { get; set; }
		public string GroupUId { get; set; }
	}

	public sealed class GetQuestionsByQuizIdQuery : IQuery<QuestionsQueryResult> {
		public string QuizUId { get; set; }
	}

	public sealed class GetQuestionByIdQuery : IQuery<QuestionQueryResult> {
		public string UId { get; set; }
	}
}
