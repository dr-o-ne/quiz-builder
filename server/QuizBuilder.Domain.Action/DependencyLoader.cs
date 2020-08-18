using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.CQRS.ActionHandlers;
using QuizBuilder.Domain.Action.Client.Services;
using QuizBuilder.Domain.Action.Client.Services.Default;
using QuizBuilder.Domain.Action.Common.Services;
using QuizBuilder.Domain.Action.Common.Services.Default;

namespace QuizBuilder.Domain.Action {

	public static class DependencyLoader {

		public static void AddServices( this IServiceCollection services ) {
			services.AddSingleton<IPageInfoDataFactory, PageInfoDataFactory>();
			services.AddSingleton<IJwtTokenFactory, JwtTokenFactory>();
		}

		public static void AddMappers( this IServiceCollection services ) {
			services.AddAutoMapper( typeof( DependencyLoader ).Assembly );
		}

		public static void AddHandlers( this IServiceCollection services ) {
			services.AddCommandQueryHandlers( typeof( ICommandHandler<> ) );
			services.AddCommandQueryHandlers( typeof( ICommandHandler<,> ) );
			services.AddCommandQueryHandlers( typeof( IQueryHandler<,> ) );
		}

		private static void AddCommandQueryHandlers( this IServiceCollection services, Type handlerInterface ) {
			var handlers = typeof( DependencyLoader ).Assembly.GetTypes()
				.Where( t => t.GetInterfaces()
					.Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface )
				);

			foreach( var handler in handlers ) {
				services.AddScoped(
					handler.GetInterfaces()
						.First( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface ), handler );
			}
		}



	}
}
