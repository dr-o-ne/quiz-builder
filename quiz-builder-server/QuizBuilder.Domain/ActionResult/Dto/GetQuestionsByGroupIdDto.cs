using System.Collections.Generic;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult.Dto {
	public class GetQuestionsByGroupIdDto {
		public IEnumerable<QuestionViewModel> Questions { get; }

		public GetQuestionsByGroupIdDto( IEnumerable<QuestionViewModel> questions ) {
			Questions = questions;
		}
	}
}
