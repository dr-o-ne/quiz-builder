using System;
using Newtonsoft.Json;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public abstract class Question : AuditableEntity<Guid> {

		[JsonIgnore]
		public abstract Enums.QuestionType Type { get; }

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public virtual string Text { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public abstract bool IsValid();

		public virtual Question Clone() {
			return (Question)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}

	}
}
