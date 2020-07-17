using System.Collections.Immutable;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {
	public sealed class QuizViewModel {

		public QuizViewModel() {
			Groups = ImmutableArray<GroupViewModel>.Empty;
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsPrevButtonEnabled { get; set; }
		public ImmutableArray<GroupViewModel> Groups { get; set; }
	}
}
