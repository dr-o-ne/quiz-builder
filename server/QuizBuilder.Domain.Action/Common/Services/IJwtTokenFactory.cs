using QuizBuilder.Data.Dto;

namespace QuizBuilder.Domain.Action.Common.Services {

	public interface IJwtTokenFactory {

		string Create( UserDto user );

	}

}
