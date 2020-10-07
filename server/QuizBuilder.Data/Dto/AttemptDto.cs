using System;

namespace QuizBuilder.Data.Dto {

	public sealed class AttemptDto {

		public long Id { get; set; }

		public string UId { get; set; }

		public long QuizId { get; set; }

		public string QuizUId { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string Data { get; set; }

		public decimal? Result { get; set; } 

	}

}
