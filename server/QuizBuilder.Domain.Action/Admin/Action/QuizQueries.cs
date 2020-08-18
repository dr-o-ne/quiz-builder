using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetAllQuizzesQuery : IQuery<QuizzesQueryResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

	}

	public sealed class GetQuizByIdQuery : IQuery<QuizQueryResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string UId { get; set; }

	}

}
