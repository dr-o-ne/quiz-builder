using System.Collections.Generic;

namespace QuizBuilder.Model.Model.Default {

	public abstract class QuizEntity : AuditableEntity<long> {

		protected QuizEntity() {
			Items = new List<QuizEntity>();
		}

		public List<QuizEntity> Items { get; set; }

	}

}
