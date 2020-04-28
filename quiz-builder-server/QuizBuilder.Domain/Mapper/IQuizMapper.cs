using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper {

	public interface IQuizMapper {

		public QuizDto Map( Quiz entity );

		public Quiz Map( QuizDto dto );

	}

}
