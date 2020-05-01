using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper {

	public interface IQuizMapper {

		QuizDto Map( Quiz entity );

		Quiz Map( QuizDto dto );

		QuizDto Map( CreateQuizCommand command );

		QuizDto Map( UpdateQuizCommand command );
	}

}
