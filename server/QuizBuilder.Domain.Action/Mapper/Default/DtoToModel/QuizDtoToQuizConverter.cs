using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.DtoToModel {

	internal sealed class QuizDtoToQuizConverter : ITypeConverter<QuizDto, Quiz> {
		public Quiz Convert( QuizDto source, Quiz destination, ResolutionContext context ) {

			var quiz = JsonSerializer.Deserialize<Quiz>( source.Settings );
			quiz.Id = source.Id;
			quiz.UId = source.UId;
			quiz.Name = source.Name;
			quiz.IsEnabled = source.IsEnabled;

			return quiz;
		}
	}
}
