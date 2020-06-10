using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Data.DataProviders;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.ActionHandler.QuizHandlers.QueryHandlers {

	public sealed class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, QuizzesQueryResult> {

		private readonly IMapper _mapper;
		private readonly IQuizDataProvider _quizDataProvider;

		public GetAllQuizzesQueryHandler( IMapper mapper, IQuizDataProvider quizDataProvider ) {
			_mapper = mapper;
			_quizDataProvider = quizDataProvider;
		}

		public async Task<QuizzesQueryResult> HandleAsync( GetAllQuizzesQuery query ) {
			IEnumerable<QuizDto> quizDtos = await _quizDataProvider.GetAll();
			IEnumerable<Quiz> quizzes = _mapper.Map<IEnumerable<QuizDto>, IEnumerable<Quiz>>( quizDtos );
			IEnumerable<QuizViewModel> quizViewModels = _mapper.Map<IEnumerable<Quiz>, IEnumerable<QuizViewModel>>( quizzes );

			return new QuizzesQueryResult{ Quizzes = quizViewModels.ToImmutableList() };
		}
	}
}
