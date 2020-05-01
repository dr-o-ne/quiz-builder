using AutoMapper;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class QuestionToQuestionConverter : ITypeConverter<Question, Question> {
		public Question Convert( Question source, Question destination, ResolutionContext context ) {
			if( source is null || destination is null )
				return null;

			if( source == destination ) { // ToDo: override Equals
				return destination;
			}

			destination.Name = source.Name;

			return destination;
		}
	}
}
