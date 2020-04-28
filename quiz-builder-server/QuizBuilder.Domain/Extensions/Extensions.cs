using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Mapper.Default;

namespace QuizBuilder.Domain.Extensions {
	public static class Extensions {
		public static void AddMappers( this IServiceCollection services ) {
			services.AddSingleton<IQuizMapper, QuizMapper>();
			services.AddSingleton<IQuestionMapper, QuestionMapper>();
		}

		public static void AddHandlers( this IServiceCollection services ) {
			services.AddCommandQueryHandlers( typeof(ICommandHandler<>) );
			services.AddCommandQueryHandlers( typeof(ICommandHandler<,>) );
			services.AddCommandQueryHandlers( typeof(IQueryHandler<,>) );
		}

		private static void AddCommandQueryHandlers( this IServiceCollection services, Type handlerInterface ) {
			var handlers = typeof(Common.Extensions.Extensions).Assembly.GetTypes()
				.Where( t => t.GetInterfaces().Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface )
				);

			foreach( var handler in handlers ) {
				services.AddScoped(
					handler.GetInterfaces()
						.First( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface ), handler );
			}
		}
	}
}
