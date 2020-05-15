using AutoMapper;
using QuizBuilder.Domain.Actions;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default.ActionToModel {

	internal sealed class UpdateQuizCommandToQuizConverter : ITypeConverter<UpdateQuizCommand, Quiz> {

		public Quiz Convert( UpdateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {UId = source.UId, IsVisible = source.IsVisible, Name = source.Name};
		}
	}
}
