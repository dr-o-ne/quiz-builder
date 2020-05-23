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
	public sealed class Workflow5 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow5( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _httpClient.PostValueAsync<QuizCommandResult>( "/quizzes/", new { Name = "Quiz 1" } );
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
			(HttpStatusCode statusCode, QuestionCommandResult data) result2 = await _httpClient.PostValueAsync<QuestionCommandResult>( "/questions/", content1 );
			string questionUId1 = result2.data.Question.Id;

			// Create Question 2
			var content2 = new {
				QuizId = uid1,
				Name = "Question Name 2",
				Text = "Question Text 2",
				Type = 1,
				Feedback = "Feedback",
				CorrectFeedback = "Correct Feedback",
				IncorrectFeedback = "Incorrect Feedback",
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionCommandResult data) result3 = await _httpClient.PostValueAsync<QuestionCommandResult>( "/questions/", content2 );
			string questionUId2 = result3.data.Question.Id;

			// Create Attempt 1
			var content = new { QuizId = uid1 };
			(HttpStatusCode statusCode, QuizAttemptCommandResult data) result4 = await _httpClient.PostValueAsync<QuizAttemptCommandResult>( "/attempts/", content );
			Assert.Equal( HttpStatusCode.Created, result4.statusCode );

			Assert.False( string.IsNullOrWhiteSpace( result4.data.QuizAttempt.Id ) );
			Assert.Equal( uid1, result4.data.QuizAttempt.QuizId );
			Assert.Equal( 2, result4.data.Questions.Count );
			Assert.DoesNotContain( "\"isCorrect\": true", result4.data.Questions[0].Choices );
			Assert.DoesNotContain( "\"isCorrect\": false", result4.data.Questions[0].Choices );
			Assert.Null( result4.data.Questions[1].Feedback );
			Assert.Null( result4.data.Questions[1].CorrectFeedback );
			Assert.Null( result4.data.Questions[1].IncorrectFeedback );
		}

		public void Dispose() => _db.Cleanup();
	}
}
