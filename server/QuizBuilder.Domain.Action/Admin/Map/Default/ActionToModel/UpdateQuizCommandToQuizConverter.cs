using System;
using AutoMapper;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ActionToModel {

	internal sealed class UpdateQuizCommandToQuizConverter : ITypeConverter<UpdateQuizCommand, Quiz> {

		public Quiz Convert( UpdateQuizCommand source, Quiz destination, ResolutionContext context ) {
			return new Quiz {
				UId = source.UId,
				IsEnabled = source.IsEnabled,
				Name = source.Name,
				PageSettings = source.PageSettings,
				QuestionsPerPage = source.QuestionsPerPage,
				IsPrevButtonEnabled = source.IsPrevButtonEnabled,
				RandomizeGroups = source.RandomizeGroups,
				RandomizeQuestions = source.RandomizeQuestions,
				IsScheduleEnabled = source.IsScheduleEnabled,
				StartDate = source.StartDate == null ? (DateTime?)null : DateTimeOffset.FromUnixTimeSeconds( source.StartDate.Value ).UtcDateTime,
				EndDate = source.EndDate == null ? (DateTime?)null : DateTimeOffset.FromUnixTimeSeconds( source.EndDate.Value ).UtcDateTime,
			};
		}
	}
}
