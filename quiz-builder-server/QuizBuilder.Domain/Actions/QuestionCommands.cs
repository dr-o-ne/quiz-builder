using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.Actions {

	public sealed class CreateQuestionCommand : ICommand<CommandResult> {

		[Required]
		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

		[JsonPropertyName( "GroupId" )]
		public string GroupUId { get; set; }

		public Enums.QuestionType Type { get; set; }

		public string Text { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public string Settings { get; set; }

		public string Choices { get; set; }

	}

	public sealed class UpdateQuestionCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		public string Name { get; set; }

		public long GroupId { get; set; }

		public Enums.QuestionType Type { get; set; }

		public string Text { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public string Settings { get; set; }

		public string Choices { get; set; }

	}

	public sealed class DeleteQuestionCommand : ICommand<CommandResult> {

		public string UId { get; set; }

	}

}
