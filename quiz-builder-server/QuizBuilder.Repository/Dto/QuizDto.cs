using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QuizBuilder.Repository.Dto {

	[Table( "Quiz" )]
	public sealed class QuizDto {

		[IgnoreDataMember]
		public long Id { get; set; }

		public string UId { get; set; }

		public string Name { get; set; }

		public bool IsVisible { get; set; }

	}

}
