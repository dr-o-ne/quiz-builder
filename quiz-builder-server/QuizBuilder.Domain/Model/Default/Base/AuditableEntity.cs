using System;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Base {

	public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity {

		[JsonIgnore]
		public DateTime CreatedDate { get; set; }

		[JsonIgnore]
		public string CreatedBy { get; set; }

		[JsonIgnore]
		public DateTime UpdatedDate { get; set; }

		[JsonIgnore]
		public string UpdatedBy { get; set; }

	}
}
