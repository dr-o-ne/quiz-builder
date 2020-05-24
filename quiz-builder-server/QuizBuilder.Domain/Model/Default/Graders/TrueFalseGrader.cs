﻿using System.Linq;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class TrueFalseGrader : IQuestionGrader<TrueFalseQuestion, TrueFalseAnswer> {

		public double Grade( TrueFalseQuestion question, TrueFalseAnswer answer ) {
			if( question.IsValid() || answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			if( answer.ChoiceSelections.Count > 2 )
				return 0;

			if( answer.ChoiceSelections.Count( x => x.IsSelected ) != 1 )
				return 0;

			BinaryChoiceSelection selectedChoice = answer.ChoiceSelections.Single( x => x.IsSelected );
			if( selectedChoice.Id == question.TrueChoice.Id && question.TrueChoice.IsCorrect == true )
				return 1;
			if( selectedChoice.Id == question.FalseChoice.Id && question.FalseChoice.IsCorrect == true )
				return 1;

			return 0;

		}

	}

}