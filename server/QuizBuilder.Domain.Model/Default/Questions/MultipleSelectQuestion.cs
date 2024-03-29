﻿using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Domain.Model.Default.Choices;
using static QuizBuilder.Domain.Model.Default.Enums;
using static QuizBuilder.Domain.Model.Default.Enums.ChoicesDisplayType;
using static QuizBuilder.Domain.Model.Default.Enums.ChoicesEnumerationType;
using static QuizBuilder.Domain.Model.Default.Enums.QuestionGradingType;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public sealed class MultipleSelectQuestion : Question {

		public override QuizItemType Type { get => MultiSelect; }

		public List<BinaryChoice> Choices { get; set; } = new List<BinaryChoice>();

		public List<BinaryChoice> GetChoicesRandomized() {
			var temp = new List<BinaryChoice>( Choices );
			if( Randomize )
				temp.Shuffle();
			return temp;
		}

		public bool Randomize { get; set; }

		public QuestionGradingType GradingType { get; set; } = AllOrNothing;

		public ChoicesDisplayType ChoicesDisplayType { get; set; } = Vertical;

		public ChoicesEnumerationType ChoicesEnumerationType { get; set; } = NoEnumeration;

		public void AddChoice( BinaryChoice choice ) {
			choice.Order = Choices.Count;
			Choices.Add( choice );
		}

		public override bool IsValid() {

			if( string.IsNullOrWhiteSpace( Text ) )
				return false;

			if( Choices.Any( x => !x.IsValid() ) )
				return false;

			if( Choices.Count < 2 )
				return false;

			if( Choices.Count( x => x.IsCorrect == true ) == 0 )
				return false;

			return true;
		}
	}
}
