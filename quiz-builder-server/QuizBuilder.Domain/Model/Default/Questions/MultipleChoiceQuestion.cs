using System;
using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.Attempts;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Utils.Extensions;
using static QuizBuilder.Domain.Model.Enums;
using static QuizBuilder.Domain.Model.Enums.ChoicesDisplayType;
using static QuizBuilder.Domain.Model.Enums.ChoicesEnumerationType;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

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

		public ChoicesDisplayType ChoicesDisplayType { get; set; } = Vertical;

		public ChoicesEnumerationType ChoicesEnumerationType { get; set; } = NoEnumeration;

		public void AddChoice( BinaryChoice choice ) {
			choice.Order = Choices.Count;
			Choices.Add( choice );
		}

		public override QuestionType Type { get => MultiChoice; }

		public MultipleChoiceQuestion WithoutCorrectChoices() {
			foreach( var item in Choices )
				item.IsCorrect = null;
			return this;
		}

		public long Match( QuestionAttempt attempt ) {

			if( !IsValid() )
				return 0;

			if( !attempt.IsValid() )
				return 0;

			if( attempt.BinaryAnswers == null )
				return 0;

			if( attempt.BinaryAnswers.Any() )
				return 0;

			if( attempt.BinaryAnswers.Count != Choices.Count )
				return 0;


			return 0;

		}

		public override bool IsValid() =>
			!string.IsNullOrWhiteSpace( Text ) &&
			Choices.All(  x => x.IsCorrect != null ) &&
		    Choices.Count( x => x.IsCorrect != null && x.IsCorrect.Value ) == 1 &&
		    Choices.Count( x => !x.IsValid() ) == 0;
	}

}
