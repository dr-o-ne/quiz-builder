using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers {
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
