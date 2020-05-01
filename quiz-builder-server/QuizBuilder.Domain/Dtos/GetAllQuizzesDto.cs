using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Domain.Model.View;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Dtos {
	public class GetAllQuizzesDto {
		public ImmutableList<QuizViewModel> Quizzes { get; }

		public GetAllQuizzesDto( IEnumerable<QuizViewModel> quizzes ) {
			Quizzes = quizzes.ToImmutableList();
		}
	}
}
