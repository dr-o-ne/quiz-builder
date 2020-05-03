using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Questions {
	public abstract class Question : AuditableEntity<long> {
		public string Name { get; set; }
		public string Text { get; set; }
		public virtual bool IsValid() => !string.IsNullOrWhiteSpace( Text );

		public virtual Question Clone() {
			return (Question)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}

		public abstract Enums.QuestionType Type { get; }
	}
}
