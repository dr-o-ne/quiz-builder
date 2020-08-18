﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.CQRS.Actions;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateQuizCommand : ICommand<QuizCommandResult> {

		[Required]
		public string Name { get; set; }

	}

	public sealed class UpdateQuizCommand : ICommand<CommandResult> {

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

	}

	public sealed class DeleteQuizCommand : ICommand<CommandResult> {

		[Required]
		public string UId { get; set; }

	}

	public sealed class DeleteQuizzesCommand : ICommand<CommandResult> {

		[JsonPropertyName( "Ids" )]
		public string[] UIds { get; set; }

	}

}
