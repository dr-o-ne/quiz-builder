using System.Collections.Immutable;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Action.Client.ActionResult {

	public sealed class StartQuizAttemptCommandResult : CommandResult<AttemptInfo> {
	}

	public sealed class AttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "quiz" )]
		public QuizAttemptInfo Quiz { get; set; }

	}

	public sealed class QuizAttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "groups" )]
		public ImmutableArray<GroupAttemptInfo> Groups { get; set; }

	}

	public sealed class GroupAttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		public ImmutableArray<QuestionAttemptInfo> Questions { get; set; }

	}

	public sealed class QuestionAttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

	}

}
