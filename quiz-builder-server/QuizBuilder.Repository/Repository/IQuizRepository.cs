using QuizBuilder.Model.Model.Default;

namespace QuizBuilder.Repository.Repository
{
    public interface IQuizRepository : IGenericRepository<Quiz>
    {
        Quiz GetById(long id);
    }
}
