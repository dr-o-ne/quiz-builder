using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Mapper.Default;

namespace QuizBuilder.Domain.Extensions {
	public static class Extensions {
		public static void AddMappers( this IServiceCollection services ) {
			services.AddSingleton<IQuestionMapper, QuestionMapper>();
			services.AddAutoMapper( typeof(Extensions).Assembly );
		}

		public static void AddHandlers( this IServiceCollection services ) {
			services.AddCommandQueryHandlers( typeof(ICommandHandler<>) );
			services.AddCommandQueryHandlers( typeof(ICommandHandler<,>) );
			services.AddCommandQueryHandlers( typeof(IQueryHandler<,>) );
		}

		private static void AddCommandQueryHandlers( this IServiceCollection services, Type handlerInterface ) {
			var handlers = typeof(Extensions).Assembly.GetTypes()
				.Where( t => t.GetInterfaces()
					.Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface )
				);

			foreach( var handler in handlers ) {
				services.AddScoped(
					handler.GetInterfaces()
						.First( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface ), handler );
			}
		}

		public static TResult Map<TResult>( this IMapper mapper, params object[] objects ) {
			TResult result = mapper.Map<TResult>( objects.First() );
			return objects.Skip( 1 ).Aggregate( result, ( res, obj ) => mapper.Map( obj, res ) );
		}

		public static TCurrent Merge<TUpdated, TCurrent>( this IMapper mapper, TUpdated updated, TCurrent current ) where TUpdated : TCurrent {
			return mapper.Map<TUpdated, TCurrent>( updated, current );
		}
	}
}
