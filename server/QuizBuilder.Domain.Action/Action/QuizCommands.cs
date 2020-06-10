using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.ActionResult;

namespace QuizBuilder.Domain.Action.Action {

	public sealed class CreateQuizCommand : ICommand<QuizCommandResult> {

		[Required]
		public string Name { get; set; }

		public bool IsVisible { get; set; }

	}

	public sealed class UpdateQuizCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		[Required]
		public string Name { get; set; }

		public bool IsVisible { get; set; }

	}

	public sealed class DeleteQuizCommand : ICommand<CommandResult> {

		[Required]
		public string UId { get; set; }

	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult> {

		[JsonPropertyName( "Ids" )]
		public string[] UIds { get; set; }

	}

}
