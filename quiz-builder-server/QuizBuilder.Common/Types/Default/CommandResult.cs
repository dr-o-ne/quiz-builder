namespace QuizBuilder.Common.Types.Default {

	public sealed class CommandResult : ICommandResult {

		public bool Success { get; }
		public string Message { get; }

		public CommandResult( bool success, string message ) {
			Success = success;
			Message = message;
		}

	}
}
