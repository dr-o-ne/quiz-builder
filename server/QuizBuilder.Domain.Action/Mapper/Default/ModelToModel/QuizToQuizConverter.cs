using AutoMapper;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.ModelToModel {
	internal sealed class QuizToQuizConverter : ITypeConverter<Quiz, Quiz> {
		public Quiz Convert( Quiz source, Quiz destination, ResolutionContext context ) {
			if( source is null || destination is null )
				return null;

			if( source == destination ) {
				// ToDo: override Equals
				return destination;
			}

			destination.Name = source.Name;
			destination.IsEnabled = source.IsEnabled;
			destination.IsPrevButtonEnabled = source.IsPrevButtonEnabled;
			destination.PageSettings = source.PageSettings;

			return destination;
		}
	}
}
