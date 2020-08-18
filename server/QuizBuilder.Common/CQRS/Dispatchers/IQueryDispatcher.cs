using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.Dispatchers {

	internal interface IQueryDispatcher {

		Task<TResult> QueryAsync<TResult>( IQuery<TResult> query );

	}

}
