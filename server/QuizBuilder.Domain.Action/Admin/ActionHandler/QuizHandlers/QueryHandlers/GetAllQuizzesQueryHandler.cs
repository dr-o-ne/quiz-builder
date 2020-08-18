using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using QuizBuilder.Common.CQRS.ActionHandlers;
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
			ImmutableArray<QuizDto> quizDtos = await _quizDataProvider.GetAll();
			List<Quiz> quizzes = _mapper.Map<List<Quiz>>( quizDtos );
			List<QuizViewModel> quizViewModels = _mapper.Map<List<QuizViewModel>>( quizzes );

			return new QuizzesQueryResult {Quizzes = quizViewModels.ToImmutableList()};
		}
	}
}
