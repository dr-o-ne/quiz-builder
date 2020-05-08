using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Repository.Repository;
using QuizBuilder.Repository.Repository.Default;

namespace QuizBuilder.Repository.Extensions {

	public static class Extensions {

		public static void AddRepositories( this IServiceCollection services ) {
			services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
			services.AddSingleton( typeof( IGenericQueryBuilder<> ), typeof( GenericQueryBuilder<> ) );
			services.AddSingleton( typeof(IGenericRepository<>), typeof(GenericRepository<>) );
		}

	}

}
