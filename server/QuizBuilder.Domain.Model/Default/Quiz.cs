using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Base;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Model.Default {

	public sealed class Quiz : AuditableEntity<long> {

		[JsonIgnore]
		public string UId { get; set; }

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public bool IsEnabled { get; set; }

		public bool IsPrevButtonEnabled { get; set; }

		public PageSettings PageSettings { get; set; }

		public bool RandomizeQuestions { get; set; }

		public long QuestionsPerPage { get; set; }

	}
}
