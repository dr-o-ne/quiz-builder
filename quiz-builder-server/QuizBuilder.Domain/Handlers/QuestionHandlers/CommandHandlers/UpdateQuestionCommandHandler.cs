using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {
	public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, CommandResult> {

		private readonly IQuestionMapper _questionMapper;
		private readonly IGenericRepository<QuestionDto> _questionRepository;

		public UpdateQuestionCommandHandler( IQuestionMapper questionMapper, IGenericRepository<QuestionDto> questionRepository ) {
			_questionMapper = questionMapper;
			_questionRepository = questionRepository;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuestionCommand command ) {
			QuestionDto quiz = _questionMapper.Map( command );
			int rowsAffected = await _questionRepository.UpdateAsync( quiz );

			return new CommandResult( success: rowsAffected > 0, message: string.Empty );
		}
	}
}
