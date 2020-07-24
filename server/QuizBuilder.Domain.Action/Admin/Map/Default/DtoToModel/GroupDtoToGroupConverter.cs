using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.DtoToModel {

	internal sealed class GroupDtoToGroupConverter : ITypeConverter<GroupDto, Group> {

		public Group Convert( GroupDto source, Group destination, ResolutionContext context ) {

			var result = JsonSerializer.Deserialize<Group>( source.Settings );

			result.Id = source.Id;
			result.UId = source.UId;
			result.Name = source.Name;
			result.IsEnabled = source.IsEnabled;
			result.SortOrder = source.SortOrder;

			return result;
		}
	}
}
