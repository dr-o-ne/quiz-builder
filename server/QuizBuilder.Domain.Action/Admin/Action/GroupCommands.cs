﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class CreateGroupCommand : ICommand<GroupCommandResult> {

		[Required]
		[JsonPropertyName( "QuizId" )]
		public string QuizUId { get; set; }

		[Required]
		public string Name { get; set; }

	}

	public sealed class UpdateGroupCommand : ICommand<CommandResult> {

		[Required]
		[JsonPropertyName( "Id" )]
		public string UId { get; set; }

		public string Name { get; set; }

	}

	public sealed class DeleteGroupCommand : ICommand<CommandResult> {

		public string UId { get; set; }

	}

}