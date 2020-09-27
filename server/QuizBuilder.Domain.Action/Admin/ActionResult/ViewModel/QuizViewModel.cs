using System.Collections.Immutable;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {
	public sealed class QuizViewModel {

		public QuizViewModel() {
			Groups = ImmutableArray<GroupViewModel>.Empty;
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsIntroductionEnabled { get; set; }
		public string Introduction { get; set; }
		public bool IsEnabled { get; set; }
		public PageSettings PageSettings { get; set; }
		public long QuestionsPerPage { get; set; }
		public bool IsPrevButtonEnabled { get; set; }
		public bool RandomizeGroups { get; set; }
		public bool RandomizeQuestions { get; set; }
		public bool IsScheduleEnabled { get; set; }
		public ImmutableArray<GroupViewModel> Groups { get; set; }
		public string HeaderColor { get; set; }
		public string BackgroundColor { get; set; }
		public string SideColor { get; set; }
		public string FooterColor { get; set; }
	}
}
