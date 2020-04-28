using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Dtos {
	public class GetAllQuizzesDto {
		public ImmutableList<QuizDto> Quizzes { get; }

		public GetAllQuizzesDto( IEnumerable<QuizDto> quizzes ) {
			Quizzes = quizzes.ToImmutableList();
		}
	}
}
