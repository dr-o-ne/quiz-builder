using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Utils.Extensions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {

	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;
		private readonly IUIdService _uIdService;

		public CreateQuestionCommandHandler( IMapper mapper, IGenericRepository<QuestionDto> questionRepository, IUIdService uIdService ) {
			_mapper = mapper;
			_questionRepository = questionRepository;
			_uIdService = uIdService;
		}

		public async Task<CommandResult> HandleAsync( CreateQuestionCommand command ) {
			Question question = _mapper.Map<CreateQuestionCommand, Question>( command );
			question.UId = _uIdService.GetUId();
			
			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			int rowsAffected = await _questionRepository.AddAsync( questionDto );

			return new CommandResult( success: rowsAffected.GreaterThanZero(), message: string.Empty );
		}
	}
}
