namespace QuizBuilder.Domain.Model.Default.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
