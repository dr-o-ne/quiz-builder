using AutoMapper;
using QuizBuilder.Domain.Model.Default.Structure;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Mapper.Default.ModelToViewModel {
	public class GroupToGroupViewModelConverter : ITypeConverter<Group, GroupViewModel> {
		public GroupViewModel Convert( Group source, GroupViewModel destination, ResolutionContext context ) {
			return source is null
				? null
				: new GroupViewModel { Id = source.UId, Name = source.Name };
		}
	}
}
