using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Common.Handlers.Default
{
    public class AllQuizzesDto
    {
        public ImmutableList<Quiz> Quizzes { get; }

        public AllQuizzesDto(IEnumerable<Quiz> quizzes)
        {
            Quizzes = quizzes.ToImmutableList();
        }
    }

    public class GetAllQuizzesQuery : IQuery<AllQuizzesDto>
    {
    }

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

            var entities = dtos.Select( x => _quizMapper.Map( x ) );

			var result = new AllQuizzesDto( entities );

            return result;
        }
    }
}
