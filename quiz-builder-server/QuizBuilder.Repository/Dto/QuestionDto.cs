using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBuilder.Repository.Dto {

	[Table( "Question" )]
	public sealed class QuestionDto {

		public long Id { get; set; }

		public string Name { get; set; }

		public int QuestionTypeId { get; set; }

		public string QuestionText { get; set; }

		public string Settings { get; set; }

	}
}
