using System.Collections.Immutable;
using System.Text.Json.Serialization;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {

	public sealed class QuizViewModel {

		public QuizViewModel() {
			Groups = ImmutableArray<GroupViewModel>.Empty;
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsEnabled { get; set; }
		public PageSettings PageSettings { get; set; }
		public long QuestionsPerPage { get; set; }
		public bool IsPrevButtonEnabled { get; set; }
		public bool RandomizeGroups { get; set; }
		public bool RandomizeQuestions { get; set; }
		public bool IsScheduleEnabled { get; set; }
		public ImmutableArray<GroupViewModel> Groups { get; set; }

		// Appearance
		public string HeaderColor { get; set; }
		public string BackgroundColor { get; set; }
		public string SideColor { get; set; }
		public string FooterColor { get; set; }

		// Start Page
		[JsonPropertyName( "isStartPageEnabled" )]
		public bool IsStartPageEnabled { get; set; }

		[JsonPropertyName( "description" )]
		public string Description { get; set; }

		[JsonPropertyName( "isTotalAttemptsEnabled" )]
		public bool IsTotalAttemptsEnabled { get; set; }

		[JsonPropertyName( "isTimeLimitEnabled" )]
		public bool IsTimeLimitEnabled { get; set; }

		[JsonPropertyName( "isTotalQuestionsEnabled" )]
		public bool IsTotalQuestionsEnabled { get; set; }

		[JsonPropertyName( "isPassingScoreEnabled" )]
		public bool IsPassingScoreEnabled { get; set; }

		// Result Page
		[JsonPropertyName( "resultPassText" )]
		public string ResultPassText { get; set; }

		[JsonPropertyName( "resultFailText" )]
		public string ResultFailText { get; set; }

		[JsonPropertyName( "IsResultPageEnabled" )]
		public bool IsResultPageEnabled { get; set; }

		[JsonPropertyName( "isResultTotalScoreEnabled" )]
		public bool IsResultTotalScoreEnabled { get; set; }

		[JsonPropertyName( "isResultPassFailEnabled" )]
		public bool IsResultPassFailEnabled { get; set; }

		[JsonPropertyName( "isResultFeedbackEnabled" )]
		public bool IsResultFeedbackEnabled { get; set; }

		[JsonPropertyName( "isResultDurationEnabled" )]
		public bool IsResultDurationEnabled { get; set; }
	}
	
}
