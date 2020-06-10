using AutoMapper;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Mapper.Default.ActionToModel {

	internal sealed class CreateGroupCommandToGroupConverter : ITypeConverter<CreateGroupCommand, Group> {

		public Group Convert( CreateGroupCommand source, Group destination, ResolutionContext context ) {
			return new Group { Name = source.Name };
		}

	}
}
