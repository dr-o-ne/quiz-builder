using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetQuestionByIdQuery : IQuery<CommandResult<QuestionViewModel>>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string UId { get; set; }

	}

	public sealed class GetQuestionTemplateQuery : IQuery<CommandResult<QuestionViewModel>>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "type" )]
		public QuizItemType Type { get; set; }

	}

}
