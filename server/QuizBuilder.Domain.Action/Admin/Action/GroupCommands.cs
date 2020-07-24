using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateGroupCommand : ICommand<GroupCommandResult> {

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

	}

	public sealed class UpdateGroupCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "selectAllQuestions" )]
		public bool SelectAllQuestions { get; set; }

		[JsonPropertyName( "randomizeQuestions" )]
		public bool RandomizeQuestions { get; set; }

		[JsonPropertyName( "countOfQuestionsToSelect" )]
		public int? CountOfQuestionsToSelect { get; set; }

	}

	public sealed class UpdateGroupNameCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "groupId" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

	}

	public sealed class DeleteGroupCommand : ICommand<CommandResult> {

		[Required]
		public string UId { get; set; }

	}

	public sealed class ReorderGroupsCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "quizId" )]
		public string QuizUId { get; set; }

		[Required]
		[JsonPropertyName( "groupIds" )]
		public string[] GroupUIds { get; set; }

	}

}
