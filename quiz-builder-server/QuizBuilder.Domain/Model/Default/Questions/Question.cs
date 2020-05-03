using Newtonsoft.Json;
using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public abstract class Question : AuditableEntity<long> {

		[JsonIgnore]
		public Enums.QuestionType Type { get; set; } //TODO: to abstract readonly?

		[JsonIgnore]
		public string Name { get; set; }

		[JsonIgnore]
		public virtual string Text { get; set; }

		public abstract bool IsValid();

		public virtual Question Clone() {
			return (Question)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}

	}
}
