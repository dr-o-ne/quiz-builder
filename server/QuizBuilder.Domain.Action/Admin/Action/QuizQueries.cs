using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetAllQuizzesQuery : IQuery<QuizzesQueryResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

	}

	public sealed class GetQuizByIdQuery : IQuery<CommandResult<QuizViewModel>>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string UId { get; set; }

	}

}
