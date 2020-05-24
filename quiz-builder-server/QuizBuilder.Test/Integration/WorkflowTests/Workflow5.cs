using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Action.ViewModel;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Domain.Model.Default.ChoiceSelections;
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

			// Start Attempt 1
			var content3 = new { QuizId = uid1 };
			(HttpStatusCode statusCode, StartQuizAttemptCommandResult data) result4 = await _httpClient.PostValueAsync<StartQuizAttemptCommandResult>( "/attempts/", content3 );
			Assert.Equal( HttpStatusCode.Created, result4.statusCode );

			Assert.False( string.IsNullOrWhiteSpace( result4.data.QuizAttempt.Id ) );
			Assert.Equal( uid1, result4.data.QuizAttempt.QuizId );
			Assert.Equal( 2, result4.data.Questions.Count );
			Assert.DoesNotContain( "\"isCorrect\": true", result4.data.Questions[0].Choices );
			Assert.DoesNotContain( "\"isCorrect\": false", result4.data.Questions[0].Choices );
			Assert.Null( result4.data.Questions[1].Feedback );
			Assert.Null( result4.data.Questions[1].CorrectFeedback );
			Assert.Null( result4.data.Questions[1].IncorrectFeedback );


			var contentObj = new EndQuizAttemptCommand {
				UId = "123", QuestionAnswers = new List<QuestionAttemptViewModel> {
					new QuestionAttemptViewModel()
				}
			};
			contentObj.QuestionAnswers[0].QuestionUId = "3234234";
			contentObj.QuestionAnswers[0].BinaryChoiceSelections = new List<BinaryChoiceSelection> {};
			contentObj.QuestionAnswers[0].BinaryChoiceSelections.Add( new BinaryChoiceSelection() {Id = 1, IsSelected = true } );
			contentObj.QuestionAnswers[0].BinaryChoiceSelections.Add( new BinaryChoiceSelection() { Id = 2, IsSelected = false } );

			// EndAttempt 1
			var content4 = @"{
   ""Id"":""123"",
   ""QuestionAnswers"":[
      {
         ""QuestionId"":""3234234"",
         ""BinaryChoiceSelections"":[
            {
               ""Id"":1,
               ""IsSelected"":true
            },
            {
               ""Id"":2,
               ""IsSelected"":false
            }
         ]
      }
   ]
}";
			var result5 = await _httpClient.PutValueAsync<EndQuizAttemptCommandResult>( "/attempts/", content4 );

		}

		public void Dispose() => _db.Cleanup();
	}
}
