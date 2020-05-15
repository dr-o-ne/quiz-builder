using AutoMapper;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default.ActionToModel {

	internal sealed class CreateQuizCommandToQuizConverter : ITypeConverter<CreateQuizCommand, Quiz> {

		public Quiz Convert( CreateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {Name = source.Name, IsVisible = source.IsVisible};
		}
	}
}
