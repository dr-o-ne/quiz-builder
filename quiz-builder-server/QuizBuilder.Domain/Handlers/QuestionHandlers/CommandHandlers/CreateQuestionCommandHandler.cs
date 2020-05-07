using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {
	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, CommandResult> {
		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public CreateQuestionCommandHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( CreateQuestionCommand command ) {
			Question question = _mapper.Map<CreateQuestionCommand, Question>( command );
			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			Guid result = await _questionRepository.AddAsync( questionDto );

			return new CommandResult( success: result != Guid.Empty, message: string.Empty );
		}
	}
}
