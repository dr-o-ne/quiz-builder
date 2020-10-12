using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateQuizCommand : ICommand<CommandResult<QuizViewModel>>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string Name { get; set; }

	}

	public sealed class UpdateQuizCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		[Required]
		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[Required]
		[JsonPropertyName( "isEnabled" )]
		public bool IsEnabled { get; set; }

		[Required]
		[JsonPropertyName( "pageSettings" )]
		public PageSettings PageSettings { get; set; }

		[Required]
		[JsonPropertyName( "questionsPerPage" )]
		public long QuestionsPerPage { get; set; }

		[Required]
		[JsonPropertyName( "isPrevButtonEnabled" )]
		public bool IsPrevButtonEnabled { get; set; }

		[Required]
		[JsonPropertyName( "randomizeGroups" )]
		public bool RandomizeGroups { get; set; }

		[Required]
		[JsonPropertyName( "randomizeQuestions" )]
		public bool RandomizeQuestions { get; set; }

		[Required]
		[JsonPropertyName( "isScheduleEnabled" )]
		public bool IsScheduleEnabled { get; set; }

		[JsonPropertyName( "startDate" )]
		public long? StartDate { get; set; }

		[JsonPropertyName( "endDate" )]
		public long? EndDate { get; set; }

		// Appearance

		[JsonPropertyName( "headerColor" )]
		public string HeaderColor { get; set; }

		[JsonPropertyName( "backgroundColor" )]
		public string BackgroundColor { get; set; }

		[JsonPropertyName( "sideColor" )]
		public string SideColor { get; set; }

		[JsonPropertyName( "footerColor" )]
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

		[JsonPropertyName( "isResultPageEnabled" )]
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

	public sealed class DeleteQuizCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[Required]
		public string UId { get; set; }

	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult>, IIdentityAction {

		public long OrgId { get; set; }

		public string UserId { get; set; }

		[JsonPropertyName( "Ids" )]
		public string[] UIds { get; set; }

	}

}
