using System.Collections.Generic;
using System.Collections.Immutable;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {

	public class GetAllQuestionsDto {
		public ImmutableList<QuestionViewModel> Questions { get; }

		public GetAllQuestionsDto( IEnumerable<QuestionViewModel> questions ) {
			Questions = questions.ToImmutableList();
		}
	}
}
