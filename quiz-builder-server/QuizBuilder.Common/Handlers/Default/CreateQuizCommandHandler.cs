using System;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Handlers.Default
{
    public class CreateQuizCommandResult : ICommandResult
    {
        public bool Success => true;
        public string Message => "Test";
        public object ResponseId => Guid.NewGuid();
    }

    public class CreateQuizCommand : ICommand<CreateQuizCommandResult>
    {
        public Guid Id { get; set; }
    }

    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CreateQuizCommandResult>
    {
        public async Task<CreateQuizCommandResult> HandleAsync(CreateQuizCommand command)
        {
            return await Task.Run(() => new CreateQuizCommandResult());
        }
    }
}
