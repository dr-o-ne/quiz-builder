namespace QuizBuilder.Model.Model
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
