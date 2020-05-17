using System.Collections.Immutable;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Dtos {

	public sealed class GetAllQuizzesDto {

		public ImmutableList<QuizViewModel> Quizzes { get; set; }

	}
}
