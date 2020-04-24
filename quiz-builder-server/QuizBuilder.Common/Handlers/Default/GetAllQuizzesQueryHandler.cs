using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using QuizBuilder.Common.Types;
using QuizBuilder.Model.Model.Default;
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
        private readonly IGenericRepository<Quiz> _quizRepository;

        public GetAllQuizzesQueryHandler( IGenericRepository<Quiz> quizRepository )
        {
            _quizRepository = quizRepository;
        }

        public async Task<AllQuizzesDto> HandleAsync(GetAllQuizzesQuery query)
        {
            var entities = await _quizRepository.GetAllAsync();
            var result = new AllQuizzesDto(entities);

            return result;
        }
    }
}
