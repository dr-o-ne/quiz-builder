using System.Collections.Generic;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult.Dto {
	public class GetQuestionsByQuizIdDto {
		public IEnumerable<QuestionViewModel> Questions { get; }

		public GetQuestionsByQuizIdDto( IEnumerable<QuestionViewModel> questions ) {
			Questions = questions;
		}
	}
}
