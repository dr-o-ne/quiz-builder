using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	public sealed class QuizIsDisabledWorkflow : _WorkflowBase {

		public QuizIsDisabledWorkflow( TestApplicationFactory<Startup> factory ) : base( factory ) {
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _apiClient.QuizCreate( new { Name = nameof( QuizIsDisabledWorkflow ) } );
			string uid1 = result1.data.Quiz.Id;

			// Create Attempt 1
			(HttpStatusCode statusCode, QuizAttemptInfo data) attemptResult1 = await _apiClient.AttemptStart( uid1 );
			Assert.Null( attemptResult1.data );

			// Enable Quiz 1
			await _apiClient.QuizUpdate( new {Id = uid1, Name = nameof(QuizIsDisabledWorkflow), IsEnabled = true, PageSettings = 1} );

			// Create Attempt 2
			(HttpStatusCode statusCode, QuizAttemptInfo data) attemptResult2 = await _apiClient.AttemptStart( uid1 );
			Assert.NotNull( attemptResult2.data );
		}
	}
}
