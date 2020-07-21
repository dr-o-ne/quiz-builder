using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default.Attempts;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToDto {

	internal sealed class QuizAttemptToAttemptDtoConverter : ITypeConverter<QuizAttempt, AttemptDto> {

		public AttemptDto Convert( QuizAttempt source, AttemptDto destination, ResolutionContext context ) =>
			new AttemptDto {
				Id = source.Id,
				UId = source.UId,
				QuizUId = source.QuizUId,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				Data = source.Data,
				Result = source.Result
			};
	}
}
