using System.Collections.Immutable;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {

	public class GroupViewModel {

		public GroupViewModel() {
			Questions = ImmutableArray<QuestionViewModel>.Empty;
		}

		public string Id { get; set; }
		public string Name { get; set; }

		public ImmutableArray<QuestionViewModel> Questions { get; set; }
	}
}
