using System.Data;

namespace QuizBuilder.Repository.Repository {

	public interface IDatabaseConnectionFactory {

		IDbConnection GetConnection();

	}

}
