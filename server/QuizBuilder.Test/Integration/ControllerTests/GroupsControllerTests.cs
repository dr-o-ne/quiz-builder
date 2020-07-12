using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.ControllerTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class GroupsControllerTests : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {
				Id = 1,
				UId = "quiz-1",
				Name = "Quiz 1",
				IsEnabled = true
			}
		}.ToImmutableArray();

		private readonly ApiClient _apiClient;
		private readonly TestDatabaseWrapper _db;

		public GroupsControllerTests( TestApplicationFactory<Startup> factory ) {
			_apiClient = new ApiClient( factory.CreateClient() );
			_db = factory.GetTestDatabaseWrapper();
			SetupData();
		}

		[Fact]
		public async Task Group_Create_Created_Test() {

			(HttpStatusCode statusCode, GroupCommandResult data) result = await _apiClient.GroupCreate( new { QuizId = "quiz-1", Name = "Group Name" } );

			Assert.Equal( HttpStatusCode.Created, result.statusCode );
		}

		[Fact]
		public async Task Group_Create_NoParent_UnprocessableEntity_Test() {

			(HttpStatusCode statusCode, GroupCommandResult data) result = await _apiClient.GroupCreate( new { QuizId = "quiz-empty", Name = "Group Name" } );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, result.statusCode );
		}

		private void SetupData() {
			_db.Cleanup();
			_db.Insert( "Quiz", QuizData );
		}

		public void Dispose() => _db.Cleanup();
	}

}
