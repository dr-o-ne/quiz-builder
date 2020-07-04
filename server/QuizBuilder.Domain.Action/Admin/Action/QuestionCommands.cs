using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateQuestionCommand : ICommand<QuestionCommandResult> {

		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

		[JsonPropertyName( "GroupId" )]
		public string GroupUId { get; set; }

		public Enums.QuestionType Type { get; set; }

		public string Text { get; set; }

		public decimal? Points { get; set; }

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

		[JsonPropertyName( "GroupId" )]
		public string GroupUId { get; set; }

		public Enums.QuestionType Type { get; set; }

		public string Text { get; set; }

		public decimal? Points { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public string Settings { get; set; }

		public string Choices { get; set; }

	}

	public sealed class DeleteQuestionCommand : ICommand<CommandResult> {

		[Required]
		public string QuizUId { get; set; }

		[Required]
		public string UId { get; set; }

	}

	public sealed class ReorderQuestionsCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "groupId" )]
		public string GroupUId { get; set; }

		[Required]
		[JsonPropertyName( "questionIds" )]
		public string[] QuestionUIds { get; set; }

	}

}
