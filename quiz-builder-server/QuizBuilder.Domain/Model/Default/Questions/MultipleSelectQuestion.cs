using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Utils.Extensions;
using static QuizBuilder.Domain.Model.Enums;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class MultipleSelectQuestion : Question {

		public List<BinaryChoice> Choices { get; set; } = new List<BinaryChoice>();

		public List<BinaryChoice> GetChoicesRandomized() {
			var temp = new List<BinaryChoice>( Choices );
			if( Randomize )
				temp.Shuffle();
			return temp;
		}

		public bool Randomize { get; set; }

		public QuestionGradingType GradingType { get; set; } = QuestionGradingType.AllOrNothing;

		public ChoicesDisplayType ChoicesDisplayType { get; set; } = ChoicesDisplayType.Vertical;

		public ChoicesEnumerationType ChoicesEnumerationType { get; set; } = ChoicesEnumerationType.NoEnumeration;

		public void AddChoice( BinaryChoice choice ) {
			choice.Order = Choices.Count;
			Choices.Add( choice );
		}

		public override QuestionType Type { get => QuestionType.MultiSelect; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			Choices.Count( x => x.IsCorrect ) > 0 &&
			Choices.Count( x => !x.IsValid() ) == 0;
	}
}
