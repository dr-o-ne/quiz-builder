using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Domain.ActionResult {

	public sealed class CreateGroupCommandResult : ICommandResult {

		public bool Success { get; }

		public string Message { get; }

		[JsonPropertyName( "groupId" )]
		public string GroupUId { get; }

		public CreateGroupCommandResult( bool success, string message, string groupUId = "" ) {
			Message = message;
			GroupUId = groupUId;
			Success = success;
		}
	}

}
