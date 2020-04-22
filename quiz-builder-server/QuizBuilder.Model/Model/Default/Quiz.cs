using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model.Default {

	[Table( "Quiz" )]
	public sealed class Quiz : AuditableEntity<long> {
		[Required]
		[MaxLength( 100 )]
		public string Name { get; set; }
	}

}
