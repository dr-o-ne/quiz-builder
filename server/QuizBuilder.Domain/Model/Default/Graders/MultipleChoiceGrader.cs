using System.Linq;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class MultipleChoiceGrader : IQuestionGrader<MultipleChoiceQuestion, MultipleChoiceAnswer> {

		public double Grade( MultipleChoiceQuestion question, MultipleChoiceAnswer answer ) {

			if( !question.IsValid() || !answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			if( answer.ChoiceSelections.Count > question.Choices.Count )
				return 0;

			if( answer.ChoiceSelections.Count( x => x.IsSelected ) != 1 )
				return 0;

			BinaryChoiceSelection selectedChoice = answer.ChoiceSelections.Single( x => x.IsSelected );
			BinaryChoice correctChoice = question.Choices.Single( x => x.IsCorrect == true );

			if( selectedChoice.Id != correctChoice.Id )
				return 0;

			return 1;

		}

	}

}
