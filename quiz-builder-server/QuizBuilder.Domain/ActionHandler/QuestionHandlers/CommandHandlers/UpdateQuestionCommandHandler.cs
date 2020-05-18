using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public UpdateQuestionCommandHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<CommandResult> HandleAsync( UpdateQuestionCommand command ) {

			QuestionDto currentQuestionDto = await _questionDataProvider.Get( command.UId );

			Question question = _mapper.Map<UpdateQuestionCommand, Question>( command );

			question.Id = currentQuestionDto.Id;
			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			await _questionDataProvider.Update( questionDto );

			return new CommandResult( success: true, message: string.Empty );

		}
	}
}
