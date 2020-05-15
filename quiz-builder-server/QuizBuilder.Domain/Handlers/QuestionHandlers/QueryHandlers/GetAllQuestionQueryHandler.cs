using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Handlers.QuestionHandlers.QueryHandlers {

	public sealed class GetAllQuestionQueryHandler : IQueryHandler<GetAllQuestionQuery, GetAllQuestionsDto> {

		private readonly IMapper _mapper;
		private readonly IQuestionDataProvider _questionDataProvider;

		public GetAllQuestionQueryHandler( IMapper mapper, IQuestionDataProvider questionDataProvider ) {
			_mapper = mapper;
			_questionDataProvider = questionDataProvider;
		}

		public async Task<GetAllQuestionsDto> HandleAsync( GetAllQuestionQuery query ) {
			var questionDtos = await _questionDataProvider.GetByQuiz("");
			IEnumerable<Question> questions = _mapper.Map<IEnumerable<QuestionDto>, IEnumerable<Question>>( questionDtos );
			IEnumerable<QuestionViewModel> questionViewModels = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>( questions );

			return new GetAllQuestionsDto( questionViewModels );
		}
	}
}
