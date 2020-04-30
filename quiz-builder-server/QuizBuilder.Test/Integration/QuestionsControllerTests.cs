using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.Data;
using Xunit;

namespace QuizBuilder.Test.Integration {

	public sealed class QuestionsControllerTests : IClassFixture<TestApplicationFactory<Startup>> {

		private readonly HttpClient _httpClient;

		public QuestionsControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			SetupData( factory.DB.ConnectionFactory );
		}

		[Fact]
		public async Task Questions_Get_Success_Test() {
			string value = await _httpClient.GetStringAsync( "/questions/1" );
			Assert.True( value.Contains( "True/False" ) );
		}

		private void SetupData( IDbConnectionFactory connectionFactory ) {
			using var connection = connectionFactory.CreateDbConnection();
			connection.Open();
			connection.DropAndCreateTable<QuestionDto>( "Question" );

			connection.Insert( "Question", new QuestionDto {
				Id = 1,
				Name = "True/False",
				QuestionTypeId = 1,
				Settings = @"{""TrueChoice"":{""IsCorrect"":false,""Text"":""TrueIncorrect""},""FalseChoice"":{""IsCorrect"":true,""Text"":""FalseCorrect""},""CreatedDate"":""0001-01-01T00:00:00"",""CreatedBy"":null,""UpdatedDate"":""0001-01-01T00:00:00"",""UpdatedBy"":null,""Id"":0}",
				QuestionText = ""
			} );
		}

	}
}
