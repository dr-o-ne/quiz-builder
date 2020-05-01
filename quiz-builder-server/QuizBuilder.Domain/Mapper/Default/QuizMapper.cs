using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Domain.Mapper.Default {

	public sealed class QuizMapper : IQuizMapper {

		public QuizDto Map( Quiz entity ) {

			if( entity == null )
				return null;

			return new QuizDto {
				Id = entity.Id,
				Name = entity.Name,
				IsVisible = entity.IsVisible
			};
		}

		public Quiz Map( QuizDto dto ) {

			if( dto == null )
				return null;

			return new Quiz {
				Id = dto.Id,
				Name = dto.Name,
				IsVisible = dto.IsVisible
			};

		}

		public QuizDto Map( CreateQuizCommand command ) {

			if( command is null )
				return null;

			return new QuizDto {
				Name = command.Name
			};
		}

		public QuizDto Map( UpdateQuizCommand command ) {

			if( command is null )
				return null;

			return new QuizDto {
				Id = command.Id,
				Name = command.Name
			};

		}
	}
}
