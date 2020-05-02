using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QuizBuilder.Repository.Dto {

	[Table( "Question" )]
	public sealed class QuestionDto {

		[IgnoreDataMember]
		public long Id { get; set; }

		public string Name { get; set; }

		public int QuestionTypeId { get; set; }

		public string QuestionText { get; set; }

		public string Settings { get; set; }

	}
}
