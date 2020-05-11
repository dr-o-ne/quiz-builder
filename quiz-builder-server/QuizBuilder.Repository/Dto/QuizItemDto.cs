using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QuizBuilder.Repository.Dto {

	[Table( "QuizItem" )]
	public sealed class QuizItemDto {

		[IgnoreDataMember]
		public long Id { get; set; }

		public string UId { get; set; }

		public long? ParentQuizItemId { get; set; }

		public long? QuestionId { get; set; }

		public int QuizItemTypeId { get; set; }

	}
}
