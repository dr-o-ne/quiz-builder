using System.Collections.Generic;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {
	public class GetQuestionsByGroupIdDto {
		public IEnumerable<QuestionViewModel> Questions { get; }

		public GetQuestionsByGroupIdDto( IEnumerable<QuestionViewModel> questions ) {
			Questions = questions;
		}
	}
}
