using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Handlers.Default
{
    public class AllQuizzesDto
    {
        public Quiz[] Quizzes => new[]
        {
            new Quiz()
            {
                Id = 1,
                Name = "First Quiz"
            },
            new Quiz()
            {
                Id = 2,
                Name = "Second Quiz"
            },
            new Quiz()
            {
                Id = 3,
                Name = "Third Quiz"
            },
        };
    }

    public class GetAllQuizzesQuery : IQuery<AllQuizzesDto>
    {

    }

    public class GetAllQuizzesQueryHandler : IQueryHandler<GetAllQuizzesQuery, AllQuizzesDto>
    {
        public Task<AllQuizzesDto> HandleAsync(GetAllQuizzesQuery query)
            => Task.Run(() => new AllQuizzesDto());
    }
}
