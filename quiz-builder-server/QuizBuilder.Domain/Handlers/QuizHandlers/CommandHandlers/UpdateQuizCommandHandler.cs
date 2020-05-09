using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Extensions;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {
	public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, CommandResult> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public UpdateQuizCommandHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuizCommand command ) {
			QuizDto quizDto = await _quizRepository.GetByIdAsync( command.Id );
			Quiz currentQuiz = _mapper.Map<QuizDto, Quiz>( quizDto );
			Quiz updatedQuiz = _mapper.Map<UpdateQuizCommand, Quiz>( command );
			Quiz mergedQuiz = _mapper.Merge( updatedQuiz, currentQuiz );
			QuizDto mergedQuizDto = _mapper.Map<Quiz, QuizDto>( mergedQuiz );
			int rowsAffected = await _quizRepository.UpdateAsync( mergedQuizDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
