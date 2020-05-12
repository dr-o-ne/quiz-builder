using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBuilder.Repository.Dto {
	[Table( "QuizQuizItem" )]
	public sealed class QuizQuizItemDto {
		public long QuizId { get; set; }

		public long QuizItemId { get; set; }
	}
}
