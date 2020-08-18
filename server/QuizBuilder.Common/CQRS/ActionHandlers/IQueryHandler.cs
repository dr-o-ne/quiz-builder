using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.ActionHandlers {

	public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult> {

		Task<TResult> HandleAsync( TQuery query );

	}

}
