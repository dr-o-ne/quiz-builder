using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Model.Mapper {

	public interface IQuizMapper {

		public QuizDto Map( Quiz entity );

		public Quiz Map( QuizDto dto );

	}

}
