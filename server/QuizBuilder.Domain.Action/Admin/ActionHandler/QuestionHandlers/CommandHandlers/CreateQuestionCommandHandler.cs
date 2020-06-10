using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils.Services;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, QuestionCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;

		public CreateQuestionCommandHandler( IMapper mapper, IUIdService uIdService, IQuizDataProvider quizDataProvider, IQuestionDataProvider questionDataProvider, IStructureDataProvider structureDataProvider, IGroupDataProvider groupDataProvider ) {
			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<QuestionCommandResult> HandleAsync( CreateQuestionCommand command ) {

			Question question = _mapper.Map<CreateQuestionCommand, Question>( command );
			question.UId = _uIdService.GetUId();

			if( !question.IsValid() )
				return new QuestionCommandResult { Success = false };

			QuizDto quizDto = await _quizDataProvider.Get( command.QuizUId );
			if( quizDto == null )
				return new QuestionCommandResult { Success = false };

			QuestionDto questionDto = _mapper.Map<Question, QuestionDto>( question );
			(long questionId, long quizItemId) ids = await _questionDataProvider.Add( questionDto );

			if( !string.IsNullOrWhiteSpace( command.GroupUId ) ) {
				GroupDto groupDto = await _groupDataProvider.Get( command.GroupUId );
				if( groupDto != null ) {
					await _structureDataProvider.AddGroupQuestionRelationship(groupDto.Id, ids.quizItemId);
				}
			}

			await _structureDataProvider.AddQuizQuestionRelationship( quizDto.Id, ids.quizItemId );

			var addedQuestion = _mapper.Map<QuestionDto, Question>( questionDto );
			var questionViewModel = _mapper.Map<Question, QuestionViewModel>( addedQuestion );

			return new QuestionCommandResult { Success = true, Question = questionViewModel };
		}
	}
}


