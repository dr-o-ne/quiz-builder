using System.Collections.Immutable;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult {

	public sealed class QuestionQueryResult {

		public QuestionViewModel Question { get; set; }

	}

	public class QuestionsQueryResult {

		public ImmutableList<QuestionViewModel> Questions { get; set; }

	}

}
