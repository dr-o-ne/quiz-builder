using System;
using System.Threading.Tasks;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Common.CQRS.Actions;

namespace QuizBuilder.Common.CQRS.Dispatchers.Default {

	internal sealed class QueryDispatcher : IQueryDispatcher {

		private readonly IServiceProvider _serviceProvider;

		public QueryDispatcher( IServiceProvider serviceProvider ) {
			_serviceProvider = serviceProvider;
		}

		public async Task<TResult> QueryAsync<TResult>( IQuery<TResult> query ) {
			Type handlerType = typeof(IQueryHandler<,>)
				.MakeGenericType( query.GetType(), typeof(TResult) );

			dynamic handler = _serviceProvider.GetService( handlerType );

			return await handler.HandleAsync( (dynamic)query );
		}

	}
}
