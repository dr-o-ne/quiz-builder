using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Model.Model;

namespace QuizBuilder.Model.Extensions {

	public static class Extensions {

		private static readonly Random Rng = new Random();

		public static void Shuffle<T>( this IList<T> list ) {
			int n = list.Count;
			while( n > 1 ) {
				n--;
				int k = Rng.Next( n + 1 );
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		public static void AddQuizBuilderDataContext( this IServiceCollection services ) {
			services.AddDbContext<QuizBuilderDataContext>( options =>
				options.UseSqlite(
					@"Data Source=..\QuizBuilder.db",
					b => b.MigrationsAssembly( "QuizBuilder.Model" ) )
			);
		}
	}
}
