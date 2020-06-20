using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Graders {

	public sealed class LongAnswerGrader : IQuestionGrader<LongAnswerQuestion, LongAnswerAnswer> {

		public double Grade( LongAnswerQuestion question, LongAnswerAnswer answer ) {

			if( !question.IsValid() || !answer.IsValid() )
				return 0;

			if( question.UId != answer.QuestionUId )
				return 0;

			return !string.IsNullOrWhiteSpace( answer.Text ) ? 1 : 0;

		}
	}
}
