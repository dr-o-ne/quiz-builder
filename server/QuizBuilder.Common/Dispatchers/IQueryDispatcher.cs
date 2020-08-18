using System.Threading.Tasks;
using QuizBuilder.Common.Types;


namespace QuizBuilder.Common.Dispatchers {

	internal interface IQueryDispatcher {

		Task<TResult> QueryAsync<TResult>( IQuery<TResult> query );

	}

}
