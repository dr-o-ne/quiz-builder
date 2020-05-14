using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {

	public sealed class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, GetAllQuizzesDto> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetAllQuizzesQueryHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<GetAllQuizzesDto> HandleAsync( GetAllQuizzesQuery query ) {
			IEnumerable<QuizDto> quizDtos = await _quizDataProvider.GetAll();
			IEnumerable<Quiz> quizzes = _mapper.Map<IEnumerable<QuizDto>, IEnumerable<Quiz>>( quizDtos );
			IEnumerable<QuizViewModel> quizViewModels = _mapper.Map<IEnumerable<Quiz>, IEnumerable<QuizViewModel>>( quizzes );

			return new GetAllQuizzesDto( quizViewModels );
		}
	}
}
