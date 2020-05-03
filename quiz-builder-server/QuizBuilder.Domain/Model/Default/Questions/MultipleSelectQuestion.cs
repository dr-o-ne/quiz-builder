using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Utils.Extensions;

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

		public Enums.QuestionGradingType GradingType { get; set; }

		public void AddChoice( BinaryChoice choice ) {
			choice.Order = Choices.Count;
			Choices.Add( choice );
		}

		public override Enums.QuestionType Type { get => Enums.QuestionType.MultiSelect; }

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			GradingType != Enums.QuestionGradingType.None &&
			Choices.Count( x => x.IsCorrect ) > 0 &&
			Choices.Count( x => !x.IsValid() ) == 0;
	}
}
