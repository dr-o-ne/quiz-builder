using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizBuilder.Api;
using QuizBuilder.Data.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.OrmLite;
using Xunit;

namespace QuizBuilder.Test.Integration {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class GroupsControllerTests : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {
				Id = 1,
				UId = "quiz-1",
				Name = "Quiz 1",
				IsVisible = true
			}
		}.ToImmutableArray();

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public GroupsControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			SetupData();
		}

		[Fact]
		public async Task Group_Create_Success_Test() {
			var content = JsonConvert.SerializeObject( new {
				QuizId = "quiz-1",
				Name = "Group Name"
			} );

			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );
			using var response = await _httpClient.PostAsync( "/groups/", stringContent );

			Assert.Equal( HttpStatusCode.Created, response.StatusCode );
		}

		[Fact]
		public async Task Group_Create_NoParent_Quiz_Test() {
			var content = JsonConvert.SerializeObject( new {
				QuizId = "quiz-empty",
				Name = "Group Name"
			} );

			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );
			using var response = await _httpClient.PostAsync( "/groups/", stringContent );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		private void SetupData() {
			_db.Cleanup();

			using IDbConnection conn = _db.CreateDbConnection();
			conn.Open();

			conn.ExecuteSql( "SET IDENTITY_INSERT dbo.Quiz ON" );
			foreach( var item in QuizData ) {
				conn.Insert( "Quiz", item );
			}
			conn.ExecuteSql( "SET IDENTITY_INSERT dbo.Quiz OFF" );
		}

		public void Dispose() => _db.Cleanup();
	}

}
