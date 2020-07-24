using AutoMapper;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Structure;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ActionToModel {

	internal sealed class UpdateGroupCommandToGroupConverter : ITypeConverter<UpdateGroupCommand, Group> {

		public Group Convert( UpdateGroupCommand source, Group destination, ResolutionContext context ) =>
			new Group {
				UId = source.UId,
				//IsEnabled = source.IsEnabled,
				Name = source.Name,
				SelectAllQuestions = source.SelectAllQuestions,
				RandomizeQuestions = source.RandomizeQuestions,
				CountOfQuestionsToSelect = source.CountOfQuestionsToSelect
			};
	}

}
