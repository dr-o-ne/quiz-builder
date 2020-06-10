using AutoMapper;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.ActionToModel {

	internal sealed class UpdateQuizCommandToQuizConverter : ITypeConverter<UpdateQuizCommand, Quiz> {

		public Quiz Convert( UpdateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {UId = source.UId, IsVisible = source.IsVisible, Name = source.Name};
		}
	}
}
