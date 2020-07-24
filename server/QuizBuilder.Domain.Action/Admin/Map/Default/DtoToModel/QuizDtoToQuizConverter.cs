using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.DtoToModel {

	internal sealed class QuizDtoToQuizConverter : ITypeConverter<QuizDto, Quiz> {
		public Quiz Convert( QuizDto source, Quiz destination, ResolutionContext context ) {

			var result = JsonSerializer.Deserialize<Quiz>( source.Settings );
			result.Id = source.Id;
			result.UId = source.UId;
			result.Name = source.Name;
			result.IsEnabled = source.IsEnabled;

			return result;
		}
	}
}
