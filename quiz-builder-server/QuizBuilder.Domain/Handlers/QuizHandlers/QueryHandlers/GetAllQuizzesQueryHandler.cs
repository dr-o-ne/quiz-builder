using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Domain.Queries.QuizQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers.QuizHandlers.QueryHandlers {
	public class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, GetAllQuizzesDto> {

		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetAllQuizzesQueryHandler( IQuizMapper quizMapper, IGenericRepository<QuizDto> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetAllQuizzesDto> HandleAsync( GetAllQuizzesQuery query ) {
			IEnumerable<QuizDto> entities = await _quizRepository.GetAllAsync();
			IEnumerable<Quiz> quizzes = entities.Select( _quizMapper.Map );

			return new GetAllQuizzesDto( new QuizViewModel[] { new QuizViewModel() } );
		}
	}
}
