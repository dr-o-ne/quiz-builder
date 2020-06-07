using AutoMapper;
using QuizBuilder.Domain.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Mapper.Default.ModelToViewModel {
	public class GroupToGroupViewModelConverter : ITypeConverter<Group, GroupViewModel> {
		public GroupViewModel Convert( Group source, GroupViewModel destination, ResolutionContext context ) {
			return source is null
				? null
				: new GroupViewModel { Id = source.UId, Name = source.Name };
		}
	}
}
