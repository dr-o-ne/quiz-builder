namespace QuizBuilder.Common.CQRS.Actions.Default {

	public class CommandResult : ICommandResult {

		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public CommandResult() {
		}

		public CommandResult( bool isSuccess, string message ) {
			IsSuccess = isSuccess;
			Message = message;
		}

		public static CommandResult Fail( string message = "" ) =>
			new CommandResult {IsSuccess = false, Message = message};

		public static CommandResult Success( string message = "" ) =>
			new CommandResult { IsSuccess = true, Message = message };
	}

	public class CommandResult<T> : CommandResult {
		public T Payload { get; set; }
	}
}
