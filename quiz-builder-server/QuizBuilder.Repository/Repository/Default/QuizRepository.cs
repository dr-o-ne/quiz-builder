using System.Linq;
using QuizBuilder.Model.Model;
using QuizBuilder.Model.Model.Default;

namespace QuizBuilder.Repository.Repository.Default
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(QuizBuilderDataContext context) : base(context)
        {
        }

        public Quiz GetById(long id) => _dbset.FirstOrDefault(x => x.Id == id);
    }
}
