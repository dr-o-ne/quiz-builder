using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.CommandHandlers {

	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, CommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;

		public CreateQuestionCommandHandler( IMapper mapper, IUIdService uIdService, IQuizDataProvider quizDataProvider, IQuestionDataProvider questionDataProvider, IStructureDataProvider structureDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
		}

		public async Task<CommandResult> HandleAsync( CreateQuestionCommand command ) {

			Question question = _mapper.Map<CreateQuestionCommand, Question>( command );
			question.UId = _uIdService.GetUId();

			if( !question.IsValid() )
				return new CommandResult( success: false, message: string.Empty );

			QuizDto quizDto = await _quizDataProvider.Get( command.QuizUId );
			if( quizDto == null )
				return new CommandResult( success: false, message: string.Empty );

			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			(long questionId, long quizItemId) ids = await _questionDataProvider.Add( questionDto );

			//IGNORE GROUP FOR NOW
			//if( string.IsNullOrWhiteSpace( command.GroupUId ) ) {

			await _structureDataProvider.AddQuizQuestionRelationship( quizDto.Id, ids.quizItemId );

			/*} else {
				throw new NotImplementedException();
			}*/

			return new CommandResult( success: true, message: string.Empty );
		}
	}
}


