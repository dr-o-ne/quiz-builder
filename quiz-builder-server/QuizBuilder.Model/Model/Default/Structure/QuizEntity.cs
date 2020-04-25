using System.Text.Json.Serialization;
using QuizBuilder.Model.Model.Default.Base;

namespace QuizBuilder.Model.Model.Default.Structure {

	public abstract class QuizEntity : AuditableEntity<long>, IValidatable {

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public string Text { get; set; }

		public abstract bool IsValid();

	}

}
