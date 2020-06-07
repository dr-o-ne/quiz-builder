using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Handlers;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Base;
using QuizBuilder.Domain.Model.Default.Graders;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain {

	public static class DependencyLoader {

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

		public static void AddModels( this IServiceCollection services ) {
			services.AddSingleton<IQuestionGrader<MultipleChoiceQuestion, MultipleChoiceAnswer>, MultipleChoiceGrader>();
			services.AddSingleton<IQuestionGrader<MultipleSelectQuestion, MultipleSelectAnswer>, MultipleSelectGrader>();
			services.AddSingleton<IQuestionGrader<TrueFalseQuestion, TrueFalseAnswer>, TrueFalseGrader>();
		}

	}
}
