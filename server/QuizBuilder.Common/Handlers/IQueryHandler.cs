using System.Threading.Tasks;
using QuizBuilder.Common.Types;

namespace QuizBuilder.Common.Handlers
{
    public interface IQueryHandler<in TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
