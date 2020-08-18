using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateQuestionCommand : ICommand<QuestionCommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

		[Required]
		[JsonPropertyName( "GroupId" )]
		public string GroupUId { get; set; }

		public QuizItemType Type { get; set; }

		[Required]
		public string Text { get; set; }

		public decimal? Points { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public string Settings { get; set; }

		public string Choices { get; set; }

	}

	public sealed class UpdateQuestionCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		public string Name { get; set; }

		[JsonPropertyName( "GroupId" )]
		public string GroupUId { get; set; }

		public QuizItemType Type { get; set; }

		public string Text { get; set; }

		public decimal? Points { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public string Settings { get; set; }

		public string Choices { get; set; }

	}

	public sealed class DeleteQuestionCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string QuizUId { get; set; }

		[Required]
		public string UId { get; set; }

	}

	public sealed class ReorderQuestionsCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "groupId" )]
		public string GroupUId { get; set; }

		[Required]
		[JsonPropertyName( "questionIds" )]
		public string[] QuestionUIds { get; set; }

	}

	public sealed class MoveQuestionCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "groupId" )]
		public string GroupUId { get; set; }

		[Required]
		[JsonPropertyName( "questionId" )]
		public string QuestionUId { get; set; }

		[Required]
		[JsonPropertyName( "questionIds" )]
		public string[] QuestionUIds { get; set; }

	}

}
