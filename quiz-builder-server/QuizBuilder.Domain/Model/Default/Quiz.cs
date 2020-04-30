using System.ComponentModel.DataAnnotations.Schema;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default {

	[Table( "Quiz" )]
	public sealed class Quiz : AuditableEntity<long> {

		public string Name { get; set; }
		public bool IsVisible { get; set; }

	}

}
