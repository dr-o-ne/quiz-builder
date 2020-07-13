using AutoMapper;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Mapper.Default.ModelToModel {

	internal sealed class QuestionToQuestionConverter : ITypeConverter<Question, Question> {

		public Question Convert( Question source, Question destination, ResolutionContext context ) {
			if( source is null || destination is null )
				return null;

			if( source == destination ) {
				// ToDo: override Equals
				return destination;
			}

			destination.Name = source.Name;
			destination.Text = source.Text;
			destination.Feedback = source.Feedback;
			destination.CorrectFeedback = source.CorrectFeedback;
			destination.IncorrectFeedback = source.IncorrectFeedback;

			return destination;
		}
	}
}
