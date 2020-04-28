using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public abstract class QuizEntity : AuditableEntity<long>, IValidatable {

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public string Text { get; set; }

		public abstract bool IsValid();

	}

}
