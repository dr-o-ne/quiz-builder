using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public sealed class DeleteQuizzesCommandHandler : ICommandHandler<DeleteQuizzesCommand, CommandResult> {

		private readonly IGenericRepository<QuizDto> _quizRepository;

		public DeleteQuizzesCommandHandler( IGenericRepository<QuizDto> quizRepository ) {
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( DeleteQuizzesCommand command ) {
			int rowsAffected = await _quizRepository.DeleteBulkAsync( command.UIds.ToList() );
			return new CommandResult( success: rowsAffected == command.UIds.Length, message: string.Empty );
		}

	}

}
