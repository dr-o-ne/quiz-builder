using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Model.Default.Base {

	public interface IQuestionGrader<in TQ, in TA>
		where TQ : Question
		where TA : Answer {

		public double Grade( TQ question, TA answer );
	}

}
