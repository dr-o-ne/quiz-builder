using AutoMapper;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper.Default
{
	public sealed class QuizDtoToQuizConverter : ITypeConverter<QuizDto, Quiz> {
		public Quiz Convert( QuizDto source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {
				Id = source.Id,
				ClientId = source.ClientId,
				Name = source.Name,
				IsVisible = source.IsVisible,
			};
		}
	}
}
