using System;

namespace QuizBuilder.Common.Types
{
    public interface ICommand
    {
        Guid Id { get; }
    }

    public interface ICommand<out TResult> where TResult : ICommandResult
    {
        Guid Id { get; }
    }
}
