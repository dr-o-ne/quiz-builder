using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public CreateQuizCommandHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _mapper.Map<CreateQuizCommand, Quiz>( command );
			quiz.ClientId = RandomIdGenerator.GetBase62( 10 );

			QuizDto quizDto = _mapper.Map<Quiz, QuizDto>( quiz );
			int rowsAffected = await _quizRepository.AddAsync( quizDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
