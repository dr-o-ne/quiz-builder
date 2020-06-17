using System.Linq;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class MultipleSelectGrader : IQuestionGrader<MultipleSelectQuestion, MultipleSelectAnswer> {

		public double Grade( MultipleSelectQuestion question, MultipleSelectAnswer answer ) {

			/*if( question.IsValid() || answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			if( answer.ChoiceSelections.Count > question.Choices.Count )
				return 0;

			if( answer.ChoiceSelections.Count( x => x.IsSelected ) != 1 )
				return 0;*/

			throw null; //TODO: depends on settings

		}

	}
}
