using AutoMapper;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToModel {
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
			destination.PageSettings = source.PageSettings;
			destination.QuestionsPerPage = source.QuestionsPerPage;
			destination.IsPrevButtonEnabled = source.IsPrevButtonEnabled;
			destination.RandomizeQuestions = source.RandomizeQuestions;

			return destination;
		}
	}
}
