using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Base {

	public abstract class Entity<T> : IEntity<T> {
		[JsonIgnore]
		public virtual T Id { get; set; }
	}

}
