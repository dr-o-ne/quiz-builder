using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {
	public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, CommandResult> {
		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<Quiz> _quizRepository;

		public UpdateQuizCommandHandler( IQuizMapper quizMapper, IGenericRepository<Quiz> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuizCommand command ) {
			Quiz quiz = _quizMapper.Map( command );
			int rowsAffected = await _quizRepository.UpdateAsync( quiz );

			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
