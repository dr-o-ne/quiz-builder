using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Action.Mapper.Default.DtoToModel {

	internal sealed class QuizDtoToQuizConverter : ITypeConverter<QuizDto, Quiz> {
		public Quiz Convert( QuizDto source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {
				Id = source.Id, UId = source.UId, Name = source.Name, IsVisible = source.IsVisible,
			};
		}
	}
}
