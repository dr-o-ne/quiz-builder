using AutoMapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Mapper.Default.ModelToViewModel {

	internal sealed class QuizToQuizViewModelConverter : ITypeConverter<Quiz, QuizViewModel> {
		public QuizViewModel Convert( Quiz source, QuizViewModel destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new QuizViewModel {Id = source.UId, Name = source.Name, IsVisible = source.IsVisible};
		}
	}
}
