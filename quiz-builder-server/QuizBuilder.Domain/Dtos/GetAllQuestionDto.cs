using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Dtos {

	public class GetAllQuestionDto {
		public ImmutableList<Question> Questions { get; }

		public GetAllQuestionDto( IEnumerable<Question> questions ) {
			Questions = questions.ToImmutableList();
		}
	}
}
