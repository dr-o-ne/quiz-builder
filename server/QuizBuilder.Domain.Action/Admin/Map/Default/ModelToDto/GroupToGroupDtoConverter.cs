using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToDto {

	internal sealed class GroupToGroupDtoConverter : ITypeConverter<Group, GroupDto> {

		public GroupDto Convert( Group source, GroupDto destination, ResolutionContext context ) =>
			new GroupDto {
				Id = source.Id,
				UId = source.UId,
				Name = source.Name,
				Settings = JsonSerializer.Serialize( source ),
				IsEnabled = source.IsEnabled
			};

	}

}
