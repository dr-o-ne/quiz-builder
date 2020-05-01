using AutoMapper;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class QuizToQuizConverter : ITypeConverter<Quiz, Quiz> {
		public Quiz Convert( Quiz source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			if( source == destination ) { // ToDo: override Equals
				return destination;
			}

			destination.Name = source.Name;
			destination.IsVisible = source.IsVisible;

			return destination;
		}
	}
}
