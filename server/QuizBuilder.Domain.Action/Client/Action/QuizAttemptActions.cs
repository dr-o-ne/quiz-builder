using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Domain.Action.Client.Action {

	public sealed class StartQuizAttemptCommand : ICommand<CommandResult<QuizAttemptInfo>> {

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

	}

	public sealed class EndQuizAttemptCommand : ICommand<CommandResult<AttemptFeedback>> {

		[Required]
		[JsonPropertyName( "id" )]
		public string AttemptUId { get; set; }

		[JsonPropertyName( "answers" )]
		public List<QuestionAttemptResult> Answers { get; set; }

	}

	public sealed class QuestionAttemptResult {

		[JsonPropertyName( "questionId" )]
		public string QuestionUId { get; set; }

		[JsonPropertyName( "choice" )]
		public long ChoiceId { get; set; }

		[JsonPropertyName( "choices" )]
		public List<long> ChoiceIds { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

	}

}
