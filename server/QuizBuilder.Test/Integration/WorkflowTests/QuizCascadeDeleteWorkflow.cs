using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	public sealed class QuizCascadeDeleteWorkflow : _WorkflowBase {

		public QuizCascadeDeleteWorkflow( TestApplicationFactory<Startup> factory ) : base( factory ) {
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _apiClient.QuizCreate( new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Group 1
			var result2 = await _apiClient.GroupCreate( new { QuizId = uid1 } );
			string uid2 = result2.data.Group.Id;

			// Create Question 1
			var result3 = await _apiClient.QuestionCreate( new {
				QuizId = uid1,
				GroupId = uid2,
				Name = "Question Name 1",
				Text = "Question Text 1",
				Type = 2,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			} );
			string uid3 = result3.data.Question.Id;

			// Delete Quiz 1
			var result4 = await _apiClient.QuizDelete( uid1 );
			Assert.Equal( HttpStatusCode.NoContent, result4.StatusCode );

			var result5 = await _apiClient.QuestionGet( uid3 );
			Assert.Equal( HttpStatusCode.NoContent, result5.statusCode );

		}

	}

}
