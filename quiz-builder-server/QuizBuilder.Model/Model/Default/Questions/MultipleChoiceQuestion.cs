using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Model.Extensions;
using QuizBuilder.Model.Model.Default.Choices;

namespace QuizBuilder.Model.Model.Default.Questions {

	public sealed class MultipleChoiceQuestion : Question {

		private readonly List<BinaryChoice> _choices = new List<BinaryChoice>();

		public List<BinaryChoice> Choices {
			get {
				var temp = new List<BinaryChoice>( _choices );
				if( Randomize )
					temp.Shuffle();
				return temp;
			}
		}

		public bool Randomize { get; set; }

		public void AddChoice( BinaryChoice choice ) {

			_choices.Add( choice );
		}

		public override bool IsValid() => base.IsValid() &&
		                                  _choices.Count( x => x.IsCorrect ) == 1 &&
		                                  _choices.Count( x => !x.IsValid() ) == 0;
	}

}
