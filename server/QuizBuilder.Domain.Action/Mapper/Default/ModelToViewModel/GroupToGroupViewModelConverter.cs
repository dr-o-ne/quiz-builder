using AutoMapper;
using QuizBuilder.Domain.Action.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Mapper.Default.ModelToViewModel {
	public class GroupToGroupViewModelConverter : ITypeConverter<Group, GroupViewModel> {
		public GroupViewModel Convert( Group source, GroupViewModel destination, ResolutionContext context ) {
			return source is null
				? null
				: new GroupViewModel { Id = source.UId, Name = source.Name };
		}
	}
}
