using System.Collections.Generic;
using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;

namespace QuizBuilder.Domain.Action.ViewModel {

	public sealed class QuestionAttemptViewModel {

		[JsonPropertyName( "QuestionId" )]
		public string QuestionUId { get; set; }

		public List<BinaryChoiceSelection> BinaryChoiceSelections { get; set; }

	}

}
