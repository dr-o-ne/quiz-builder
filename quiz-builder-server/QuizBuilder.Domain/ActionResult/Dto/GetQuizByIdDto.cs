using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.ActionResult.Dto {

	public sealed class GetQuizByIdDtoCommandResult : CommandResult {

		public GetQuizByIdDto Data { get; set; }

	}

	public sealed class GetQuizByIdDto {

		public QuizViewModel Quiz { get; set; }

	}
}
