using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class Workflow1 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly ApiClient _apiClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow1( TestApplicationFactory<Startup> factory ) {
			_apiClient = new ApiClient( factory.CreateClient() );
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _apiClient.QuizCreate( new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Quiz 2
			(HttpStatusCode statusCode, QuizCommandResult data) result2 = await _apiClient.QuizCreate( new { Name = "Quiz 2" } );
			string uid2 = result2.data.Quiz.Id;

			// Create Quiz 3
			(HttpStatusCode statusCode, QuizCommandResult data) result3 = await _apiClient.QuizCreate( new { Name = "Quiz 3" } );
			string uid3 = result3.data.Quiz.Id;
			
			// Get All Quizzes
			(HttpStatusCode statusCode, QuizzesQueryResult data) result4 = await _apiClient.QuizGetAll();
			Assert.Equal( 3, result4.data.Quizzes.Count );
			Assert.Equal( uid1, result4.data.Quizzes[0].Id );
			Assert.Equal( uid2, result4.data.Quizzes[1].Id );
			Assert.Equal( uid3, result4.data.Quizzes[2].Id );
			Assert.Equal( "Quiz 1", result4.data.Quizzes[0].Name );
			Assert.Equal( "Quiz 2", result4.data.Quizzes[1].Name );
			Assert.Equal( "Quiz 3", result4.data.Quizzes[2].Name );
			Assert.False( result4.data.Quizzes[0].IsEnabled );
			Assert.False( result4.data.Quizzes[1].IsEnabled );
			Assert.False( result4.data.Quizzes[2].IsEnabled );

			// Update Quiz 1
			await _apiClient.QuizUpdate( new { Id = uid1, Name = "Quiz 1 New Name" } );

			// Update Quiz 3
			await _apiClient.QuizUpdate( new { Id = uid3, Name = "Quiz 3", IsEnabled = true } );

			// Get All Quizzes
			(HttpStatusCode statusCode, QuizzesQueryResult data) result7 = await _apiClient.QuizGetAll();
			Assert.Equal( 3, result7.data.Quizzes.Count );
			Assert.Equal( "Quiz 1 New Name", result7.data.Quizzes[0].Name );
			Assert.True( result7.data.Quizzes[2].IsEnabled );

			// Delete Quiz 1
			using HttpResponseMessage response8 = await _apiClient.QuizDelete( uid1 );

			// Get All Quizzes
			(HttpStatusCode statusCode, QuizzesQueryResult data) result9 = await _apiClient.QuizGetAll();
			Assert.Equal( 2, result9.data.Quizzes.Count );

			using HttpResponseMessage response = await _apiClient.QuizDelete( new List<string> { uid2, uid3 } );

			// Final Check
			(HttpStatusCode statusCode, QuizzesQueryResult data) result11 = await _apiClient.QuizGetAll();
			Assert.Empty( result11.data.Quizzes );
		}

		public void Dispose() => _db.Cleanup();
	}

}
