using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;

namespace QuizBuilder.Domain.Model.Default.Answers {

	public sealed class MultipleChoiceAnswer : Answer {

		public List<BinaryChoiceSelection> ChoiceSelections { get; set; }

		public override bool IsValid() =>
			base.IsValid() &&
			ChoiceSelections != null &&
			!ChoiceSelections.Any();
	}

}
