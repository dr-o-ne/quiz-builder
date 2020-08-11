using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel {

	public sealed class GroupViewModel {

		public GroupViewModel() {
			Questions = ImmutableArray<QuestionViewModel>.Empty;
		}

		[JsonPropertyName( "id" )]
		public string Id { get; set; }

		[JsonPropertyName( "name" )]
		public string Name { get; set; }

		[JsonPropertyName( "isEnabled" )]
		public bool IsEnabled { get; set; }

		[JsonPropertyName( "randomizeQuestions" )]
		public bool RandomizeQuestions { get; set; }

		[JsonPropertyName( "countOfQuestionsToSelect" )]
		public int? CountOfQuestionsToSelect { get; set; }

		[JsonPropertyName( "selectAllQuestions" )]
		public bool SelectAllQuestions { get; set; }

		public ImmutableArray<QuestionViewModel> Questions { get; set; }
	}
}
