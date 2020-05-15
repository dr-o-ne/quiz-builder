using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Mapper.Default.ModelToDto {

	internal sealed class GroupToGroupDtoConverter : ITypeConverter<Group, GroupDto> {

		public GroupDto Convert( Group source, GroupDto destination, ResolutionContext context ) {
			return new GroupDto {Id = source.Id, UId = source.UId, Name = source.Name};
		}

	}
}
