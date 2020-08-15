using System.Data;

namespace QuizBuilder.Data.Common {

	public interface IDatabaseConnectionFactory {

		IDbConnection GetConnection();

	}
}
