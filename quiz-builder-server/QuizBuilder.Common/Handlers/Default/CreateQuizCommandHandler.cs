using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default
{
    public class CreateQuizCommandResult : ICommandResult
    {
        public bool Success { get; }
        public string Message { get; }
        public Guid CommandId { get; }

        public CreateQuizCommandResult(bool success, string message, Guid commandId)
        {
            Success = success;
            Message = message;
            CommandId = commandId;
        }
    }

    public class CreateQuizCommand : ICommand<CreateQuizCommandResult>
    {
        public Guid CommandId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CreateQuizCommandResult>
    {
        private readonly IQuizRepository _quizRepository;

        public CreateQuizCommandHandler(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<CreateQuizCommandResult> HandleAsync(CreateQuizCommand command)
        {
            var quiz = new Quiz() { Name = command.Name };
            var entity = await Task.Run(() =>
            {
                var record = _quizRepository.Add(quiz);
                _quizRepository.Save();
                return record;
            });
            var result = new CreateQuizCommandResult(
                success: entity?.Id > 0,
                message: entity?.Id > 0 ? "Success" : "Failed",
                commandId: command.CommandId);

            return result;
        }
    }
}
