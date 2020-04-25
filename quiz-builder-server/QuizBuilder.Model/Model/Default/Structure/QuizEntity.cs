using System.Collections.Generic;
using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model.Default.Structure {

	public abstract class QuizEntity : AuditableEntity<long>, IValidatable {

		public string Name { get; set; }

		public string Text { get; set; }

		protected QuizEntity() {
			Items = new List<QuizEntity>();
		}

		public List<QuizEntity> Items { get; set; }

		public abstract bool IsValid();

	}

}
