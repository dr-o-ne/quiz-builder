using AutoMapper;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Mapper.Default.ActionToModel {

	internal sealed class UpdateGroupCommandToGroupConverter : ITypeConverter<UpdateGroupCommand, Group> {

		public Group Convert( UpdateGroupCommand source, Group destination, ResolutionContext context ) {
			return new Group { UId = source.UId, Name = source.Name };
		}
	}
}

