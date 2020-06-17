using System.Linq;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class MultipleSelectGrader : IQuestionGrader<MultipleSelectQuestion, MultipleSelectAnswer> {

		public double Grade( MultipleSelectQuestion question, MultipleSelectAnswer answer ) {

			if( !question.IsValid() || !answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			if( answer.ChoiceIds.Count > question.Choices.Count )
				return 0;

			bool result = question.Choices
				.Where( x => x.IsCorrect == true )
				.Select( x => x.Id )
				.SequenceEqual(
					answer.ChoiceIds.OrderBy( a => a )
				);

			return result ? 1 : 0;
		}

	}
}
