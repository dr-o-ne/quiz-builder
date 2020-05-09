using AutoMapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper.Default
{
	public sealed class QuizToQuizDtoConverter : ITypeConverter<Quiz, QuizDto> {
		public QuizDto Convert( Quiz source, QuizDto destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new QuizDto {
				Id = source.Id,
				UId = source.UId,
				Name = source.Name,
				IsVisible = source.IsVisible
			};
		}
	}
}
