using AutoMapper;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils.Extensions;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class QuestionToQuestionConverter : ITypeConverter<Question, Question> {
		public Question Convert( Question source, Question destination, ResolutionContext context ) {
			if( source is null || destination is null )
				return null;

			if( source == destination ) { // ToDo: override Equals
				return destination;
			}

			Question merged = destination.Clone();

			merged.Name = source.Name;
			merged.Text = source.Text;

			return merged;
		}
	}
}
