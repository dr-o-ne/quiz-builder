using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Action.Client.ActionResult {

	public sealed class StartPageInfo {

		[JsonPropertyName( "id" )]
		public string UId { get; set; }

		[JsonPropertyName( "isStartPageEnabled" )]
		public bool IsStartPageEnabled { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "description" )]
		public string Description { get; set; }

		[JsonPropertyName( "totalAttempts" )]
		public int? TotalAttempts { get; set; }

		[JsonPropertyName( "timeLimit" )]
		public int? TimeLimit { get; set; }

		[JsonPropertyName( "totalQuestions" )]
		public int? TotalQuestions { get; set; }

		[JsonPropertyName( "passingScore" )]
		public decimal? PassingScore { get; set; }

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

	public sealed class AttemptFeedbackInfo {

		[JsonPropertyName( "duration" )]
		public int? Duration { get; set; }

		[JsonPropertyName( "isSuccess" )]
		public bool? IsSuccess { get; set; }

		[JsonPropertyName( "totalScore" )]
		public decimal? TotalScore { get; set; }

		[JsonPropertyName( "feedback" )]
		public string Feedback { get; set; }

	}

}
