using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Domain.Queries.QuizQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {
	public class GetQuizByIdQueryHandler : IQueryHandler<GetQuizByIdQuery, GetQuizByIdDto> {
		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetQuizByIdQueryHandler( IQuizMapper quizMapper, IGenericRepository<QuizDto> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetQuizByIdDto> HandleAsync( GetQuizByIdQuery query ) {
			QuizDto entity = await _quizRepository.GetByIdAsync( query.Id );
			Quiz quiz = _quizMapper.Map( entity );

			return entity is null ? null : new GetQuizByIdDto( new QuizViewModel() { Id = 1 } );
		}
	}
}
