using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateGroupCommand : ICommand<GroupCommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

	}

	public sealed class UpdateGroupCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "isEnabled" )]
		public bool IsEnabled { get; set; }

		[JsonPropertyName( "selectAllQuestions" )]
		public bool SelectAllQuestions { get; set; }

		[JsonPropertyName( "randomizeQuestions" )]
		public bool RandomizeQuestions { get; set; }

		[JsonPropertyName( "countOfQuestionsToSelect" )]
		public int? CountOfQuestionsToSelect { get; set; }

	}

	public sealed class DeleteGroupCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string UId { get; set; }

	}

	public sealed class ReorderGroupsCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "quizId" )]
		public string QuizUId { get; set; }

		[Required]
		[JsonPropertyName( "groupIds" )]
		public string[] GroupUIds { get; set; }

	}

}
