using System.Collections.Generic;

namespace QuizBuilder.Domain.Model.View {
	public sealed class QuizViewModel {
		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsVisible { get; set; }
		public IEnumerable<GroupViewModel> Groups { get; set; }
	}
}
