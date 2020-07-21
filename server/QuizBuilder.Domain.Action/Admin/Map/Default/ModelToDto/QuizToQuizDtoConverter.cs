using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToDto {
	internal sealed class QuizToQuizDtoConverter : ITypeConverter<Quiz, QuizDto> {
		public QuizDto Convert( Quiz source, QuizDto destination, ResolutionContext context ) {

			return new QuizDto {
				Id = source.Id,
				UId = source.UId,
				Name = source.Name,
				Settings = JsonSerializer.Serialize( source ),
				IsEnabled = source.IsEnabled
			};
		}
	}
}
