﻿using System.Collections.Immutable;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Action.Client.ActionResult {

	public sealed class StartQuizAttemptCommandResult : CommandResult<AttemptInfo> {
	}

	public sealed class EndQuizAttemptCommandResult : CommandResult<AttemptResult> {
	}

	#region Dto Input

	public sealed class AttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "quiz" )]
		public QuizAttemptInfo Quiz { get; set; }

		[JsonPropertyName( "appearance" )]
		public AppearanceInfo AppearanceInfo { get; set; }

	}

	public sealed class AppearanceInfo {

		[JsonPropertyName( "headerBackground" )]
		public string HeaderColor { get; set; }

		[JsonPropertyName( "mainBackground" )]
		public string MainColor { get; set; }

		[JsonPropertyName( "cardBackground" )]
		public string CardColor { get; set; }

		[JsonPropertyName( "footerBackground" )]
		public string FooterColor { get; set; }

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
		public long Type { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

		[JsonPropertyName( "isHtmlText" )]
		public bool IsHtmlText { get; set; }

		[JsonPropertyName( "choicesDisplayType" )]
		public long ChoicesDisplayType { get; set; }

		[JsonPropertyName( "choices" )]
		public ImmutableArray<ChoiceAttemptInfo> Choices { get; set; }

	}

	public sealed class ChoiceAttemptInfo {

		[JsonPropertyName( "id" )]
		public long Id { get; set; }

		[JsonPropertyName( "text" )]
		public string Text { get; set; }

	}

	#endregion

	#region Dto Output

	public sealed class AttemptResult {

		public double Score { get; set; }

	}

	#endregion

}
