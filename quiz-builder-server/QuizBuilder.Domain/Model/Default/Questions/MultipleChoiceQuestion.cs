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

		public void AddChoice( BinaryChoice choice ) {

			Choices.Add( choice );
		}

		public override bool IsValid() => base.IsValid() &&
		                                  Choices.Count( x => x.IsCorrect ) == 1 &&
		                                  Choices.Count( x => !x.IsValid() ) == 0;

		public override Enums.QuestionType Type => Enums.QuestionType.MultiChoice;

	}

}
