using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Structure {

	public abstract class QuizEntity : AuditableEntity<long>, IValidatable {

		[JsonIgnore]
		public string UId { get; set; }

		[JsonIgnore]
		public string ParentUId { get; set; }

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public long SortOrder { get; set; }

		public abstract bool IsValid();

	}

}
