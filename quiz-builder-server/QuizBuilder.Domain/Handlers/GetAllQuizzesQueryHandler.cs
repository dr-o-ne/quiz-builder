using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Domain.Handlers
{
	public class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, AllQuizzesDto>
	{
		private readonly IQuizMapper _quizMapper;
		private readonly IGenericRepository<QuizDto> _quizRepository;

		public GetAllQuizzesQueryHandler( IQuizMapper quizMapper, IGenericRepository<QuizDto> quizRepository ) {
			_quizMapper = quizMapper;
			_quizRepository = quizRepository;
		}

		public async Task<AllQuizzesDto> HandleAsync(GetAllQuizzesQuery query)
		{
			IEnumerable<QuizDto> dtos = await _quizRepository.GetAllAsync();

			var entities = dtos.Select( x => _quizMapper.Map( (QuizDto) x ) );

			var result = new AllQuizzesDto( entities );

			return result;
		}
	}
}
