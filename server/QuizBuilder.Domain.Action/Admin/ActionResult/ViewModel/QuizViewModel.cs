using System.Collections.Immutable;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {
	public sealed class QuizViewModel {

		public QuizViewModel() {
			Groups = ImmutableArray<GroupViewModel>.Empty;
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsEnabled { get; set; }
		public Enums.PageSettings PageSettings { get; set; }
		public bool IsPrevButtonEnabled { get; set; }
		public ImmutableArray<GroupViewModel> Groups { get; set; }
	}
}
