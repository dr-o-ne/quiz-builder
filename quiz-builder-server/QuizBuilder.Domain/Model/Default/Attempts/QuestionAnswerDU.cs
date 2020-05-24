﻿using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;

namespace QuizBuilder.Domain.Model.Default.Attempts {

	public sealed class QuestionAnswerDU : IValidatable {

		public string QuestionUId { get; set; }

		public List<BinaryChoiceSelection> BinaryChoiceSelection { get; set; }

		public string TextAnswer { get; set; }

		//Other response types?? like discriminated union TODO:

		public bool IsValid() {
			int count = 0;
			if( BinaryChoiceSelection.Any() )
				count++;
			if( !string.IsNullOrWhiteSpace( TextAnswer ) )
				count++;
			return count == 1;
		}
	}
}
