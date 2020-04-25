using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model.Default {

	public sealed class Quiz : AuditableEntity<long> {

		public string Name { get; set; }

	}

}
