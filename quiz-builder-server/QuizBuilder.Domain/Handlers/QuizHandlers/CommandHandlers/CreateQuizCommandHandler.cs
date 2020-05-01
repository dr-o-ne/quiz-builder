using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {
	public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult> {
		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<Quiz> _quizRepository;

		public CreateQuizCommandHandler( IQuizMapper quizMapper, IGenericRepository<Quiz> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _quizMapper.Map( command );
			int rowsAffected = await _quizRepository.AddAsync( quiz );

			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
