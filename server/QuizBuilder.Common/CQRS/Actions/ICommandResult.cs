namespace QuizBuilder.Common.CQRS.Actions {

	public interface ICommandResult {

		bool IsSuccess { get; }

		string Message { get; }

	}

}
