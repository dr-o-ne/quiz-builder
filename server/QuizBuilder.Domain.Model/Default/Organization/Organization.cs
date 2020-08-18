using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Organization {

	public sealed class Organization : AuditableEntity<long> {

		public string UId { get; set; }

	}

}
