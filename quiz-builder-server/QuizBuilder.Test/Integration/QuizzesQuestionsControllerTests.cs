using System.Net.Http;
using QuizBuilder.Api;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.Data;
using Xunit;

namespace QuizBuilder.Test.Integration {

	public sealed class QuizzesQuestionsControllerTests : IClassFixture<TestApplicationFactory<Startup>> {

		private readonly HttpClient _httpClient;

		public QuizzesQuestionsControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			SetupData( factory.DB.ConnectionFactory );
		}

		private static void SetupData( IDbConnectionFactory connectionFactory ) {

			/*using var connection = connectionFactory.CreateDbConnection();
			connection.Open();
			connection.DropAndCreateTable<QuestionDto>( "Question" );

			foreach( var item in QuestionData ) {
				connection.Insert( "Question", item );
			}*/
		}
	}

}
