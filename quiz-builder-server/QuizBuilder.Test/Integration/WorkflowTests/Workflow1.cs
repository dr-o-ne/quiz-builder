using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class Workflow1 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow1( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizQueryResult data) result1 = await _httpClient.PostValueAsync<QuizQueryResult>( "/quizzes/", new {Name = "Quiz 1"} );
			string uid1 = result1.data.Quiz.Id;

			// Create Quiz 2
			(HttpStatusCode statusCode, QuizQueryResult data) result2 = await _httpClient.PostValueAsync<QuizQueryResult>( "/quizzes/", new {Name = "Quiz 2"} );
			string uid2 = result2.data.Quiz.Id;

			// Create Quiz 3
			(HttpStatusCode statusCode, QuizQueryResult data) result3 = await _httpClient.PostValueAsync<QuizQueryResult>( "/quizzes/", new { Name = "Quiz 3" } );
			string uid3 = result3.data.Quiz.Id;

			// Get All Quizzes
			(HttpStatusCode statusCode, QuizzesQueryResult data) result4 = await _httpClient.GetValueAsync<QuizzesQueryResult>( "/quizzes" );
			Assert.Equal( 3, result4.data.Quizzes.Count );
			Assert.Equal( uid1, result4.data.Quizzes[0].Id );
			Assert.Equal( uid2, result4.data.Quizzes[1].Id );
			Assert.Equal( uid3, result4.data.Quizzes[2].Id );
			Assert.Equal( "Quiz 1", result4.data.Quizzes[0].Name );
			Assert.Equal( "Quiz 2", result4.data.Quizzes[1].Name );
			Assert.Equal( "Quiz 3", result4.data.Quizzes[2].Name );
			Assert.False( result4.data.Quizzes[0].IsVisible );
			Assert.False( result4.data.Quizzes[1].IsVisible );
			Assert.False( result4.data.Quizzes[2].IsVisible );

			// Update Quiz 1
			(HttpStatusCode statusCode, QuizQueryResult data) result5 = await _httpClient.PutValueAsync<QuizQueryResult>( "/quizzes/", new { Id = uid1, Name = "Quiz 1 New Name" } );

			// Update Quiz 3
			(HttpStatusCode statusCode, QuizQueryResult data) result6 = await _httpClient.PutValueAsync<QuizQueryResult>( "/quizzes/", new { Id = uid3, Name = "Quiz 3", IsVisible = true } );

			// Get All Quizzes
			(HttpStatusCode statusCode, QuizzesQueryResult data) result7 = await _httpClient.GetValueAsync<QuizzesQueryResult>( "/quizzes" );
			Assert.Equal( 3, result7.data.Quizzes.Count );
			Assert.Equal( "Quiz 1 New Name", result7.data.Quizzes[0].Name );
			Assert.True( result7.data.Quizzes[2].IsVisible );

			// Delete Quiz 1
			var response8 = await _httpClient.DeleteAsync( "/quizzes/" + uid1 );
			(HttpStatusCode statusCode, QuizzesQueryResult data) result9 = await _httpClient.GetValueAsync<QuizzesQueryResult>( "/quizzes" );
			Assert.Equal( 2, result9.data.Quizzes.Count );

			// Delete Bulk Quiz 2 and Quiz 3
			var content = JsonSerializer.Serialize( new { Ids = new List<string> { uid2, uid3 } } );
			using var request = new HttpRequestMessage {
				Method = HttpMethod.Delete,
				RequestUri = new Uri( _httpClient.BaseAddress + "quizzes/" ),
				Content = new StringContent( content, Encoding.UTF8, "application/json" )
			};
			using var response = await _httpClient.SendAsync( request );

			//Final Check
			(HttpStatusCode statusCode, QuizzesQueryResult data) result11 = await _httpClient.GetValueAsync<QuizzesQueryResult>( "/quizzes" );
			Assert.Empty( result11.data.Quizzes );
		}

		public void Dispose() => _db.Cleanup();
	}

}
