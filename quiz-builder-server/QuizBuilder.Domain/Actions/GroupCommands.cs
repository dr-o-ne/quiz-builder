using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Actions {

	public sealed class CreateGroupCommandResult : ICommandResult {
		public bool Success { get; }
		public string Message { get; }
		[JsonPropertyName( "groupId" )]
		public string GroupUId { get; }

		public CreateGroupCommandResult( bool success, string message, string groupUId = ""  ) {
			Message = message;
			GroupUId = groupUId;
			Success = success;
		}
	}

	public sealed class CreateGroupCommand : ICommand<CreateGroupCommandResult> {

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

		[Required]
		public string Name { get; set; }

	}

	public sealed class UpdateGroupCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		public string Name { get; set; }

	}

	public sealed class DeleteGroupCommand : ICommand<CommandResult> {

		public string UId { get; set; }

	}

}
