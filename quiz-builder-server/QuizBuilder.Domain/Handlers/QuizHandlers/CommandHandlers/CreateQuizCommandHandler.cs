using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {

	public sealed class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;
		private readonly IUIdService _uIdService;

		public CreateQuizCommandHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository, IUIdService uIdService ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
			_uIdService = uIdService;
		}

		public async Task<CommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _mapper.Map<CreateQuizCommand, Quiz>( command );
			quiz.UId = _uIdService.GetUId();

			QuizDto quizDto = _mapper.Map<Quiz, QuizDto>( quiz );
			int rowsAffected = await _quizRepository.AddAsync( quizDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
