namespace QuizBuilder.Common.Types.Default {

	public class CommandResult : ICommandResult {

		public bool Success { get; set; }
		public string Message { get; set; }

		public CommandResult()
		{
		}

		public CommandResult( bool success, string message ) {
			Success = success;
			Message = message;
		}

	}

	public class CommandResult<T> : CommandResult {

		public T Payload { get; set; }

	}
}
