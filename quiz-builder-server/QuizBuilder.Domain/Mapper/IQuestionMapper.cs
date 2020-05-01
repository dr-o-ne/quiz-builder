using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper {

	public interface IQuestionMapper {

		QuestionDto Map( Question entity );

		Question Map( QuestionDto dto );

		QuestionDto Map( CreateQuestionCommand command );

		QuestionDto Map( UpdateQuestionCommand command );

	}

}
