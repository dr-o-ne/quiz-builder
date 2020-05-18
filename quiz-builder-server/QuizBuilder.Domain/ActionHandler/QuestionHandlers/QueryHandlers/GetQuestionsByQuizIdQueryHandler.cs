using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.ActionResult.Dto;
using QuizBuilder.Domain.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.ActionHandler.QuestionHandlers.QueryHandlers {

	public sealed class GetQuestionsByQuizIdQueryHandler: IQueryHandler<GetQuestionsByQuizIdQuery, GetQuestionsByQuizIdDto> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetQuestionsByQuizIdQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<GetQuestionsByQuizIdDto> HandleAsync( GetQuestionsByQuizIdQuery query ) {
			IEnumerable<QuestionDto> questionDtos = await _questionDataProvider.GetByQuiz( query.QuizUId );
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			return new GetQuestionsByQuizIdDto( questionViewModels );
		}
	}
}
