using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Model.Model.Default.Choices;

namespace QuizBuilder.Model.Model.Default.Questions {

	public sealed class MultipleChoiceQuestion : Question {

		private readonly List<BinaryChoice> _choices = new List<BinaryChoice>();

		public bool Randomize { get; set; }

		public void AddChoice( BinaryChoice choice ) {

			_choices.Add( choice );
		}

		public override bool IsValid() {

			return base.IsValid() && _choices.Count( x => x.IsCorrect ) == 1;

		}

	}

}
