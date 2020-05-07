using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.CommandHandlers {
	public sealed class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CommandResult> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public CreateQuizCommandHandler( IMapper mapper, IGenericRepository<QuizDto> quizRepository ) {
			_mapper = mapper;
			_quizRepository = quizRepository;
		}

		public async Task<CommandResult> HandleAsync( CreateQuizCommand command ) {
			Quiz quiz = _mapper.Map<CreateQuizCommand, Quiz>( command );
			QuizDto quizDto = _mapper.Map<Quiz, QuizDto>( quiz );
			Guid id = await _quizRepository.AddAsync( quizDto );

			return new CommandResult( success: id != Guid.Empty, message: string.Empty );
		}
	}
}
