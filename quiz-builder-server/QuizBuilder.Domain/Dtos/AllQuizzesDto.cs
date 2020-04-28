using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Dtos
{
    public class AllQuizzesDto
    {
        public ImmutableList<Quiz> Quizzes { get; }

        public AllQuizzesDto(IEnumerable<Quiz> quizzes)
        {
            Quizzes = quizzes.ToImmutableList();
        }
    }
}
