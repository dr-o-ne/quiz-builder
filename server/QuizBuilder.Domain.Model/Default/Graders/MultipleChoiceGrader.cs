using System.Linq;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class MultipleChoiceGrader : IQuestionGrader<MultipleChoiceQuestion, MultipleChoiceAnswer> {

		public decimal Grade( MultipleChoiceQuestion question, MultipleChoiceAnswer answer ) {

			if( !question.IsValid() || !answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			BinaryChoice correctChoice = question.Choices.Single( x => x.IsCorrect == true );
			if( answer.ChoiceId != correctChoice.Id )
				return 0;

			return question.GetPoints();

		}

	}

}
