using AutoMapper;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.ActionToModel {

	internal sealed class CreateQuizCommandToQuizConverter : ITypeConverter<CreateQuizCommand, Quiz> {

		public Quiz Convert( CreateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {Name = source.Name, IsVisible = source.IsVisible};
		}
	}
}
