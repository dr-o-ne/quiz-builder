using System.Collections.Generic;
using System.Collections.Immutable;
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

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionsByQuizIdQueryHandler: IQueryHandler<GetQuestionsByQuizIdQuery, QuestionsQueryResult> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuestionsByQuizIdQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<QuestionsQueryResult> HandleAsync( GetQuestionsByQuizIdQuery query ) {
			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( query.QuizUId );
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			return new QuestionsQueryResult {Questions = questionViewModels.ToImmutableList()};
		}
	}
}
