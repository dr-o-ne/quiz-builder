using System.Text.Json.Serialization;

namespace QuizBuilder.Model.Model.Default.Base {

	public abstract class BaseEntity {
	}

	public abstract class Entity<T> : BaseEntity, IEntity<T> {

		[JsonIgnore]
		public virtual T Id { get; set; }

	}

}
