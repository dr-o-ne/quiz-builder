using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.ActionHandler.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionByParentQueryHandler : IQueryHandler<GetQuestionsByParentQuery, ImmutableList<QuestionViewModel>> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuestionByParentQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<ImmutableList<QuestionViewModel>> HandleAsync( GetQuestionsByParentQuery query ) {
			var questionDtos = await _questionDataProvider.GetByParent( query.QuizUId, query.GroupUId );
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			return questionViewModels.ToImmutableList();
		}
	}
}
