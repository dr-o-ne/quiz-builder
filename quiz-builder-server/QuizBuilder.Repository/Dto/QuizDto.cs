using System.ComponentModel.DataAnnotations.Schema;

namespace QuizBuilder.Repository.Dto {

	[Table( "Quiz" )]
	public sealed class QuizDto {

		public long Id { get; set; }

		public string Name { get; set; }

	}

}
