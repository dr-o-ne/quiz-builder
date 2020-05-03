using QuizBuilder.Domain.Model.Default.Base;

namespace QuizBuilder.Domain.Model.Default {
	public sealed class Quiz : AuditableEntity<long> {
		public string Name { get; set; }
		public bool IsVisible { get; set; }

		public Quiz Clone() {
			return (Quiz)this.MemberwiseClone(); // ToDo: deep cloning needed?
		}
	}
}
