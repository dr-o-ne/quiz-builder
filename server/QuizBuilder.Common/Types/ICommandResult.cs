namespace QuizBuilder.Common.Types
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
