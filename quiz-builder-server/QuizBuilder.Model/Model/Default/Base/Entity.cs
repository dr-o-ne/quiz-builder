namespace QuizBuilder.Model.Model.Default.Base
{
    public abstract class BaseEntity {

    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
