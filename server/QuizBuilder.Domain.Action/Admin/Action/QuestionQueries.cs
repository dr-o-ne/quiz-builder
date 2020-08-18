using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetQuestionByIdQuery : IQuery<QuestionQueryResult> {

		[Required]
		public string UId { get; set; }

	}

}
