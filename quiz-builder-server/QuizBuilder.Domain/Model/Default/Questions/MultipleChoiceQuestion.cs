using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class MultipleChoiceQuestion : Question {

		public List<BinaryChoice> Choices { get; set; } = new List<BinaryChoice>();

		public List<BinaryChoice> GetChoicesRandomized() {
			var temp = new List<BinaryChoice>( Choices );
			if( Randomize )
				temp.Shuffle();
			return temp;
		}

		public bool Randomize { get; set; }

		public Enums.ChoicesDisplayType ChoicesDisplayType { get; set; } = Enums.ChoicesDisplayType.Vertical;

		public Enums.ChoicesEnumerationType ChoicesEnumerationType { get; set; } = Enums.ChoicesEnumerationType.NoEnumeration;

		public void AddChoice( BinaryChoice choice ) {
			choice.Order = Choices.Count;
			Choices.Add( choice );
		}

		public override Enums.QuestionType Type { get => Enums.QuestionType.MultiChoice; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
		    Choices.Count( x => x.IsCorrect ) == 1 &&
		    Choices.Count( x => !x.IsValid() ) == 0;
	}

}
