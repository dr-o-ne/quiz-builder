using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {

	public sealed class GetQuizByIdDtoCommandResult : CommandResult {

		public GetQuizByIdDto Data { get; set; }

	}

	public sealed class GetQuizByIdDto {

		public QuizViewModel Quiz { get; set; }

	}
}
