using System;
using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	public sealed class Workflow6 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly ApiClient _apiClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow6( TestApplicationFactory<Startup> factory ) {
			_apiClient = new ApiClient( factory.CreateClient() );
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _apiClient.QuizCreate( new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Group 1
			var result2 = await _apiClient.GroupCreate( new { QuizId = uid1 } );
			string uid2 = result2.data.Group.Id;

			// Create Group 2
			var result3 = await _apiClient.GroupCreate( new { QuizId = uid1 } );
			string uid3 = result3.data.Group.Id;

			// Get Quiz, check group order
			var result4 = await _apiClient.QuizGet( uid1 );
			var groups = result4.data.Quiz.Groups;
			Assert.Equal( uid2, groups[0].Id );
			Assert.Equal( uid3, groups[1].Id );


		}

		public void Dispose() => _db.Cleanup();
	}
}
