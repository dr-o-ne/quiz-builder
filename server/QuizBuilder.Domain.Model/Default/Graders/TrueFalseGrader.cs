using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class TrueFalseGrader : IQuestionGrader<TrueFalseQuestion, TrueFalseAnswer> {

		public decimal Grade( TrueFalseQuestion question, TrueFalseAnswer answer ) {
			if( !question.IsValid() || !answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			if( answer.ChoiceId == question.TrueChoice.Id && question.TrueChoice.IsCorrect == true )
				return question.GetPoints();
			if( answer.ChoiceId == question.FalseChoice.Id && question.FalseChoice.IsCorrect == true )
				return question.GetPoints();

			return 0;

		}

	}

}
