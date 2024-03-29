using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, CommandResult<QuestionViewModel>> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuestionByIdQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<CommandResult<QuestionViewModel>> HandleAsync( GetQuestionByIdQuery query ) {
			QuestionDto questionDto = await _questionDataProvider.Get( query.UId );
			if( questionDto == null )
				return null;

			Question question = _mapper.Map<QuestionDto, Question>( questionDto );
			QuestionViewModel questionViewModel = _mapper.Map<Question, QuestionViewModel>( question );

			return new CommandResult<QuestionViewModel> {
				IsSuccess = true,
				Payload = questionViewModel
			};
		}
	}
}
