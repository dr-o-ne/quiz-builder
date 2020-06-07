using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
