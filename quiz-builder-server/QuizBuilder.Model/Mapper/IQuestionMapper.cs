using QuizBuilder.Model.Model.Default.Questions;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Model.Mapper {

	public interface IQuestionMapper {

		public QuestionDto Map( Question entity );

		public Question Map( QuestionDto dto );

	}

}
