using System;
using System.ComponentModel.DataAnnotations;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Domain.Commands {
	public class CreateQuizCommandResult : ICommandResult {

		public bool Success { get; }
		public string Message { get; }
		public Guid CommandId { get; }

		public CreateQuizCommandResult( bool success, string message, Guid commandId ) {
			Success = success;
			Message = message;
			CommandId = commandId;
		}
	}

	public class CreateQuizCommand : ICommand<CreateQuizCommandResult> {
		public Guid CommandId { get; set; }

		[Required] [MaxLength( 100 )] public string Name { get; set; }
	}
}
