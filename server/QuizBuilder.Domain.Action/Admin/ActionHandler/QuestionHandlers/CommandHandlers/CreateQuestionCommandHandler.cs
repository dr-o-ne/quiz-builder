using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.Services;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.CommandHandlers {

	public sealed class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, QuestionCommandResult> {

		private readonly IMapper _mapper;
		private readonly IUIdService _uIdService;
		private readonly IQuizDataProvider _quizDataProvider;
		private readonly IQuestionDataProvider _questionDataProvider;
		private readonly IStructureDataProvider _structureDataProvider;
		private readonly IGroupDataProvider _groupDataProvider;

		public CreateQuestionCommandHandler(
			IMapper mapper,
			IUIdService uIdService,
			IQuizDataProvider quizDataProvider,
			IQuestionDataProvider questionDataProvider,
			IStructureDataProvider structureDataProvider,
			IGroupDataProvider groupDataProvider ) {

			_mapper = mapper;
			_uIdService = uIdService;
			_quizDataProvider = quizDataProvider;
			_questionDataProvider = questionDataProvider;
			_structureDataProvider = structureDataProvider;
			_groupDataProvider = groupDataProvider;
		}

		public async Task<QuestionCommandResult> HandleAsync( CreateQuestionCommand command ) {

			var quizDto = await _quizDataProvider.Get( command.OrgId, command.UserId, command.QuizUId );
			if( quizDto == null )
				return new QuestionCommandResult { IsSuccess = false };

			GroupDto groupDto = await _groupDataProvider.Get( command.GroupUId );
			if( groupDto == null )
				return new QuestionCommandResult { IsSuccess = false };

			Question modelBefore = _mapper.Map<Question>( command );
			modelBefore.UId = _uIdService.GetUId();

			if( !modelBefore.IsValid() )
				return new QuestionCommandResult { IsSuccess = false };

			QuestionDto questionDtoBefore = _mapper.Map<QuestionDto>( modelBefore );
			QuestionDto questionDtoAfter = await Save( quizDto.Id, groupDto.Id, questionDtoBefore );

			var modelAfter = _mapper.Map<Question>( questionDtoAfter );
			var viewModel = _mapper.Map<QuestionViewModel>( modelAfter );

			return new QuestionCommandResult {
				IsSuccess = true,
				Question = viewModel
			};
		}

		private async Task<QuestionDto> Save( long quizId, long groupId, QuestionDto dto ) {
			QuestionDto questionDtoAfter = await _questionDataProvider.Add( groupId, dto );
			await _structureDataProvider.AddQuizQuestionRelationship( quizId, questionDtoAfter.Id );

			return questionDtoAfter;
		}
	}
}


