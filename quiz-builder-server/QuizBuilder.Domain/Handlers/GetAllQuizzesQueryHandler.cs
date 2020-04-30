using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers {

	public class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, GetAllQuizzesDto> {

		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<Quiz> _quizRepository;

		public GetAllQuizzesQueryHandler( IQuizMapper quizMapper, IGenericRepository<Quiz> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<GetAllQuizzesDto> HandleAsync( GetAllQuizzesQuery query ) {
			IEnumerable<Quiz> entities = await _quizRepository.GetAllAsync();
			IEnumerable<QuizDto> dtos = entities.Select( _quizMapper.Map );

			return new GetAllQuizzesDto( dtos );
		}
	}
}
