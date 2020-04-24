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
        private readonly IGenericRepository<Quiz> _quizRepository;

        public CreateQuizCommandHandler( IGenericRepository<Quiz> quizRepository )
        {
            _quizRepository = quizRepository;
        }

        public async Task<CreateQuizCommandResult> HandleAsync(CreateQuizCommand command)
        {
            var quiz = new Quiz() { Name = command.Name };
			long id = await _quizRepository.AddAsync( quiz );

			var result = new CreateQuizCommandResult(
                success: id > 0,
                message: id > 0 ? "Success" : "Failed",
                commandId: command.CommandId);

            return result;
        }
    }
}
