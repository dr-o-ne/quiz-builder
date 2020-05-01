using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuizBuilder.Domain.Model.Default.Base {

	public abstract class BaseEntity {
	}

	public abstract class Entity<T> : BaseEntity, IEntity<T> {
		[JsonIgnore]
		public virtual T Id { get; set; }
	}

}
