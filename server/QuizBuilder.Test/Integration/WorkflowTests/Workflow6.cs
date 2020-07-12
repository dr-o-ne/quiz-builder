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
			(HttpStatusCode statusCode, GroupCommandResult data) result2 = await _apiClient.GroupCreate( new { QuizId = uid1 } );
			string uid2 = result2.data.Group.Id;

		}

		public void Dispose() => _db.Cleanup();
	}
}
