using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {
	public class GetAllQuizzesDto {
		public ImmutableList<QuizViewModel> Quizzes { get; }

		public GetAllQuizzesDto( IEnumerable<QuizViewModel> quizzes ) {
			Quizzes = quizzes.ToImmutableList();
		}
	}
}
