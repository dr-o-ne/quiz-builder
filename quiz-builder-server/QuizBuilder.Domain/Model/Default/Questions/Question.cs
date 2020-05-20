using Newtonsoft.Json;
using QuizBuilder.Domain.Model.Default.Structure;
using static QuizBuilder.Domain.Model.Enums;

namespace QuizBuilder.Domain.Model.Default.Questions {

	public abstract class Question : QuizEntity {

		[JsonIgnore]
		public abstract QuestionType Type { get; }

		[JsonIgnore]
		public virtual string Text { get; set; }

		public string Feedback { get; set; }

		public string CorrectFeedback { get; set; }

		public string IncorrectFeedback { get; set; }

		public virtual Question Clone() {
			return (Question)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}

	}
}
