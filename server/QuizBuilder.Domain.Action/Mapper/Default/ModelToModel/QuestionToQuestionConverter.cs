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

			Question merged = destination.Clone();

			merged.Name = source.Name;
			merged.Text = source.Text;
			merged.Feedback = source.Feedback;
			merged.CorrectFeedback = source.CorrectFeedback;
			merged.IncorrectFeedback = source.IncorrectFeedback;

			return merged;
		}
	}
}
