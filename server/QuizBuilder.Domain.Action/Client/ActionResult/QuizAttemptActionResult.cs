using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Action.Client.ActionResult {

	public sealed class StartQuizAttemptCommandResult : CommandResult<QuizAttemptInfo> {
	}

	public sealed class EndQuizAttemptCommandResult : CommandResult<AttemptFeedback> {
	}

	public sealed class QuizAttemptInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "settings" )]
		public SettingsInfo SettingsInfo { get; set; }

		[JsonPropertyName( "appearance" )]
		public AppearanceInfo AppearanceInfo { get; set; }

		[JsonPropertyName( "pages" )]
		public List<PageInfo> Pages { get; set; }

	}

	public sealed class SettingsInfo {

		[JsonPropertyName( "isPrevButtonEnabled" )]
		public bool IsPrevButtonEnabled { get; set; }

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

	public sealed class PageInfo {

		public PageInfo() {
			Questions = new List<QuestionAttemptInfo>();
		}

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "questions" )]
		public List<QuestionAttemptInfo> Questions { get; set; }

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

	public sealed class AttemptFeedback {

		[JsonPropertyName( "score" )]
		public decimal Score { get; set; }

	}

}
