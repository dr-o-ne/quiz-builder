using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Base {

	public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity {
		[IgnoreDataMember]
		public DateTime CreatedDate { get; set; }

		[IgnoreDataMember]
		public string CreatedBy { get; set; }

		[IgnoreDataMember]
		public DateTime UpdatedDate { get; set; }

		[IgnoreDataMember]
		public string UpdatedBy { get; set; }
	}
}
