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

		[JsonPropertyName( "appearance" )]
		public Appearance Appearance { get; set; }

	}

	public sealed class Appearance {

		[JsonPropertyName( "mainBackground" )]
		public string MainBackground { get; set; }

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

		[JsonPropertyName( "type" )]
		public string Type { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

		[JsonPropertyName( "choices" )]
		public ImmutableArray<ChoiceAttemptInfo> Choices { get; set; }

	}

	public sealed class ChoiceAttemptInfo {

		[JsonPropertyName( "id" )]
		public long Id { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

	}



}
