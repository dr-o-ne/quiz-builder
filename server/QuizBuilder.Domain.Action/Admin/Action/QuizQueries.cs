using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetAllQuizzesQuery : IQuery<QuizzesQueryResult> {
	}

	public sealed class GetQuizByIdQuery : IQuery<QuizQueryResult> {

		[Required]
		public string UId { get; set; }

	}

}
