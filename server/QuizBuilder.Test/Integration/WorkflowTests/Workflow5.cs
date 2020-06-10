using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.ActionResult;
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
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _httpClient.PostValueAsync<QuizCommandResult>( "admin/quizzes/", new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Question 1
			var content1 = new {
				QuizId = uid1,
				Name = "Question Name 1",
				Text = "Question Text 1",
				Type = 2,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"Id\":0,\"isCorrect\":true,\"text\":\"Choice 1\"},{\"Id\":1,\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionCommandResult data) result2 = await _httpClient.PostValueAsync<QuestionCommandResult>( "admin/questions/", content1 );
			string questionUId1 = result2.data.Question.Id;

			// Create Question 2
			var content2 = new {
				QuizId = uid1,
				Name = "Question Name 2",
				Text = "Question Text 2",
				Type = 2,
				Feedback = "Feedback",
				CorrectFeedback = "Correct Feedback",
				IncorrectFeedback = "Incorrect Feedback",
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"Id\":0,\"isCorrect\":true,\"text\":\"Choice 1\"},{\"Id\":1,\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionCommandResult data) result3 = await _httpClient.PostValueAsync<QuestionCommandResult>( "admin/questions/", content2 );
			string questionUId2 = result3.data.Question.Id;

			// Start Attempt 1
			var content3 = new { QuizId = uid1 };
			(HttpStatusCode statusCode, StartQuizAttemptCommandResult data) result4 = await _httpClient.PostValueAsync<StartQuizAttemptCommandResult>( "admin/attempts/", content3 );
			Assert.Equal( HttpStatusCode.Created, result4.statusCode );

			Assert.False( string.IsNullOrWhiteSpace( result4.data.QuizAttempt.Id ) );
			Assert.Equal( uid1, result4.data.QuizAttempt.QuizId );
			Assert.Equal( 2, result4.data.Questions.Count );
			Assert.DoesNotContain( "\"isCorrect\": true", result4.data.Questions[0].Choices );
			Assert.DoesNotContain( "\"isCorrect\": false", result4.data.Questions[0].Choices );
			Assert.Null( result4.data.Questions[1].Feedback );
			Assert.Null( result4.data.Questions[1].CorrectFeedback );
			Assert.Null( result4.data.Questions[1].IncorrectFeedback );

			// End Attempt 1
			var content4 = $@"{{
   ""Id"":""{result4.data.QuizAttempt.Id}"",
   ""QuestionAnswers"":[
      {{
         ""QuestionId"":""{questionUId1}"",
         ""BinaryChoiceSelections"":[
            {{
               ""Id"":0,
               ""IsSelected"":true
            }},
            {{
               ""Id"":1,
               ""IsSelected"":false
            }}
         ]
      }}
   ]
}}";
			(HttpStatusCode statusCode, EndQuizAttemptCommandResult data) result5 = await _httpClient.PutValueAsync<EndQuizAttemptCommandResult>( "admin/attempts/", content4 );
			Assert.Equal( HttpStatusCode.OK, result5.statusCode );
			Assert.Equal( 1, result5.data.Score );

		}

		public void Dispose() => _db.Cleanup();
	}
}
