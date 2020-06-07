namespace QuizBuilder.Common.Types
{
    public interface ICommandResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
