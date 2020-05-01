using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
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

		public Quiz Map( CreateQuizCommand command ) {
			if( command is null )
				return null;

			return new Quiz {Name = command.Name};
		}

		public Quiz Map( UpdateQuizCommand command ) {
			if( command is null )
				return null;

			return new Quiz {Id = command.Id, Name = command.Name};
		}
	}
}
