using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper {

	public interface IQuestionMapper {

		public QuestionDto Map( Question entity );

		public Question Map( QuestionDto dto );

	}

}
