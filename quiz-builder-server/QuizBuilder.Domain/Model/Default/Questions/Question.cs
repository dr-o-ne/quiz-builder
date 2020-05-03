using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Questions {
	public abstract class Question : AuditableEntity<long> {
		public Enums.QuestionType Type { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public virtual bool IsValid() => !string.IsNullOrWhiteSpace( Text );

		public virtual Question Clone() {
			return (Question)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}
	}
}
