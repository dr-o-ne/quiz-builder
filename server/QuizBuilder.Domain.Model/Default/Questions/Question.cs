using System.Text.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Structure;
using static QuizBuilder.Domain.Model.Default.Enums;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public abstract class Question : QuizEntity {

		[JsonIgnore]
		public abstract QuizItemType Type { get; }

		[JsonIgnore]
		public virtual string Text { get; set; }

		public decimal? Points { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public bool IsRequired { get; set; }

		public decimal GetPoints() => Points ?? 0;

	}
}
