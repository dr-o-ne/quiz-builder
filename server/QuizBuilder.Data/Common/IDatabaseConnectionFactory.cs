using System.Data;

namespace QuizBuilder.Data.Common {

	internal interface IDatabaseConnectionFactory {

		IDbConnection GetConnection();

	}
}
