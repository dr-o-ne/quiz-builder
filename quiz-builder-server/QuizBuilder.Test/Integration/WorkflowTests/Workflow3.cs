using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class Workflow3 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow3( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizQueryResult data) result1 = await _httpClient.PostValueAsync<QuizQueryResult>( "/quizzes/", new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Question 1
			var content1 = new {
				QuizId = uid1,
				Name = "Question Name 1",
				Text = "Question Text 1",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionQueryResult data) result2 = await _httpClient.PostValueAsync<QuestionQueryResult>( "/questions/", content1 );
			string questionUId1 = result2.data.Question.Id;

			// Delete Quiz 1
			var result3 = await _httpClient.DeleteAsync( $"/quizzes/{uid1}" );

			(HttpStatusCode statusCode, QuestionQueryResult data) result4 = await _httpClient.GetValueAsync<QuestionQueryResult>( $"/questions/{questionUId1}" );
			Assert.Equal( HttpStatusCode.NoContent, result4.statusCode );
		}

		public void Dispose() => _db.Cleanup();
	}

}
