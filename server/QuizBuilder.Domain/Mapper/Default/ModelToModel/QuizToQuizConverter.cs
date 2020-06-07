using AutoMapper;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default.ModelToModel {
	internal sealed class QuizToQuizConverter : ITypeConverter<Quiz, Quiz> {
		public Quiz Convert( Quiz source, Quiz destination, ResolutionContext context ) {
			if( source is null || destination is null )
				return null;

			if( source == destination ) {
				// ToDo: override Equals
				return destination;
			}

			Quiz merged = destination.Clone();

			merged.Name = source.Name;
			merged.IsVisible = source.IsVisible;

			return merged;
		}
	}
}
