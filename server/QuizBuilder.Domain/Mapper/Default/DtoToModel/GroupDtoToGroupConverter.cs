using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Mapper.Default.DtoToModel {
	public class GroupDtoToGroupConverter : ITypeConverter<GroupDto, Group> {
		public Group Convert( GroupDto source, Group destination, ResolutionContext context ) {
			return source is null
				? null
				: new Group { UId = source.UId, Name = source.Name };
		}
	}
}
