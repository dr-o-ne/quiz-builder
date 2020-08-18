using System;

namespace QuizBuilder.Common.CQRS.Actions
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }

    public interface ICommand<out TResult> where TResult : ICommandResult
    {
        // Guid CommandId { get; }
    }
}
