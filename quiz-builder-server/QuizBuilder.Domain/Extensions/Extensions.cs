using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Handlers;

namespace QuizBuilder.Domain.Extensions {
	public static class Extensions {
		public static void AddMappers( this IServiceCollection services ) {
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

		public static TOriginal Merge<TModified, TOriginal>( this IMapper mapper, TModified modified, TOriginal original ) where TModified : TOriginal {
			return mapper.Map( modified, original );
		}
	}
}
