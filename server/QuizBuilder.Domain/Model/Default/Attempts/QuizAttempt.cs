using System;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Attempts {

	public sealed class QuizAttempt : AuditableEntity<long> {

		public string UId { get; set; }

		public string QuizUId { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Data { get; set; }

		public double Result { get; set; }

	}

}
