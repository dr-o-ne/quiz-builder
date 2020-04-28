using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper.Default {

	public sealed class QuizMapper : IQuizMapper {

		public QuizDto Map( Quiz entity ) {

			if( entity == null )
				return null;

			return new QuizDto {Id = entity.Id, Name = entity.Name};
		}

		public Quiz Map( QuizDto dto ) {
			if( dto == null )
				return null;

			return new Quiz {Id = dto.Id, Name = dto.Name};
		}
	}
}
