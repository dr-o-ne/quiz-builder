using AutoMapper;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Mapper.Default.ActionToModel {

	internal sealed class UpdateGroupCommandToGroupConverter : ITypeConverter<UpdateGroupCommand, Group> {

		public Group Convert( UpdateGroupCommand source, Group destination, ResolutionContext context ) {
			return new Group { UId = source.UId, Name = source.Name };
		}
	}
}

