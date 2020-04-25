using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default {

	public class CreateQuizCommandResult : ICommandResult {

		public bool Success { get; }
		public string Message { get; }
		public Guid CommandId { get; }

		public CreateQuizCommandResult( bool success, string message, Guid commandId ) {
			Success = success;
			Message = message;
			CommandId = commandId;
		}
	}

	public class CreateQuizCommand : ICommand<CreateQuizCommandResult> {

		public Guid CommandId { get; set; }

		[Required]
		[MaxLength( 100 )]
		public string Name { get; set; }
	}

	public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CreateQuizCommandResult> {

		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public CreateQuizCommandHandler( IQuizMapper quizMapper, IGenericRepository<QuizDto> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<CreateQuizCommandResult> HandleAsync( CreateQuizCommand command ) {
			var quiz = new Quiz {Name = command.Name};

			var dto = _quizMapper.Map( quiz );

			long id = await _quizRepository.AddAsync( dto );

			var result = new CreateQuizCommandResult(
				success: id > 0,
				message: id > 0 ? "Success" : "Failed",
				commandId: command.CommandId );

			return result;
		}
	}
}
