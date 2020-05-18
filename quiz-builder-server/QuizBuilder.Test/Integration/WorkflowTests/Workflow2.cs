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
	public sealed class Workflow2 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow2( TestApplicationFactory<Startup> factory ) {
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

			// Create Question 2
			var content2 = new {
				QuizId = uid1,
				Name = "Question Name 2",
				Text = "Question Text 2",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionQueryResult data) result3 = await _httpClient.PostValueAsync<QuestionQueryResult>( "/questions/", content2 );
			string questionUId2 = result3.data.Question.Id;

			// Get All Questions
			(HttpStatusCode statusCode, QuestionsQueryResult data) result4 = await _httpClient.GetValueAsync<QuestionsQueryResult>( $"/quizzes/{uid1}/questions/" );
			Assert.Equal( 2, result4.data.Questions.Count );
			Assert.Equal( questionUId1, result4.data.Questions[0].Id );
			Assert.Equal( questionUId2, result4.data.Questions[1].Id );
			Assert.Equal( "Question Name 1", result4.data.Questions[0].Name );
			Assert.Equal( "Question Name 2", result4.data.Questions[1].Name );

			// Update Question 2
			var content3 = new {
				Id = questionUId2,
				Name = "Question Name 2 New",
				Text = "Question Text 2 New",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			await _httpClient.PutValueAsync<QuestionQueryResult>( "/questions/", content3 );

			// Get Question 2
			(HttpStatusCode statusCode, QuestionQueryResult data) result6 = await _httpClient.GetValueAsync<QuestionQueryResult>( $"/questions/{questionUId2}" );
			Assert.Equal( "Question Name 2 New", result6.data.Question.Name );

			// Delete Question 1 //TODO: FIX DELETE
			var result7 = await _httpClient.DeleteAsync( $"/questions/{questionUId1}" );
			Assert.Equal( HttpStatusCode.NoContent, result7.StatusCode );

			// Get Question 1
			(HttpStatusCode statusCode, QuestionQueryResult data) result8 = await _httpClient.GetValueAsync<QuestionQueryResult>( $"/questions/{questionUId2}" );
			Assert.Equal( HttpStatusCode.NoContent, result8.statusCode );

		}

		public void Dispose() => _db.Cleanup();
	}
}
