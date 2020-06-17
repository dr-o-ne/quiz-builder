using System.Collections.Generic;
using System.Linq;

namespace QuizBuilder.Domain.Model.Default.Answers {

	public sealed class MultipleSelectAnswer : Answer {

		public List<long> ChoiceIds { get; set; }

		public MultipleSelectAnswer( string questionUId, List<long> choiceIds ) : base( questionUId ) {
			ChoiceIds = choiceIds;
		}

		public override bool IsValid() =>
			base.IsValid() &&
			ChoiceIds != null &&
			ChoiceIds.Any();
	}
}
