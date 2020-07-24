using AutoMapper;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToViewModel {

	public class GroupToGroupViewModelConverter : ITypeConverter<Group, GroupViewModel> {

		public GroupViewModel Convert( Group source, GroupViewModel destination, ResolutionContext context ) =>
			new GroupViewModel {
				Id = source.UId,
				Name = source.Name,
				CountOfQuestionsToSelect = source.CountOfQuestionsToSelect,
				RandomizeQuestions = source.RandomizeQuestions,
				SelectAllQuestions = source.SelectAllQuestions
			};

	}

}
