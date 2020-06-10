using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.ModelToDto {
	internal sealed class QuizToQuizDtoConverter : ITypeConverter<Quiz, QuizDto> {
		public QuizDto Convert( Quiz source, QuizDto destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new QuizDto {Id = source.Id, UId = source.UId, Name = source.Name, IsVisible = source.IsVisible};
		}
	}
}
