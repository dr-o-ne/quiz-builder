﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;

namespace QuizBuilder.Domain.Actions {

	public sealed class CreateGroupCommand : ICommand<CommandResult> {

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
