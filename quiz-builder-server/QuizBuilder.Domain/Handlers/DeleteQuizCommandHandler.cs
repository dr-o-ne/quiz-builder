using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers {
	public class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, CommandResult> {
		private readonly IGenericRepository<Quiz> _quizRepository;

		public DeleteQuizCommandHandler( IGenericRepository<Quiz> quizRepository ) {
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizCommand command ) {
			int rowsAffected = await _quizRepository.DeleteAsync( command.Id );
			var t = rowsAffected;
			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
