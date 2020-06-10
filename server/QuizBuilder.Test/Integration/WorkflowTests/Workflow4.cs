﻿using System;
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
	public sealed class Workflow4 : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public Workflow4( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			_db.Cleanup();
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _httpClient.PostValueAsync<QuizCommandResult>( "admin/quizzes/", new { Name = "Quiz 1" } );
			string uid1 = result1.data.Quiz.Id;

			// Create Group 1
			var content = new { QuizId = uid1, Name = "Group Name" };
			(HttpStatusCode statusCode, GroupCommandResult data) result = await _httpClient.PostValueAsync<GroupCommandResult>( "admin/groups/", content );
			string uid2 = result.data.Group.Id;

			// Create Question 1
			var content1 = new {
				QuizId = uid1,
				GroupId = uid2,
				Name = "Question Name 1",
				Text = "Question Text 1",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};
			(HttpStatusCode statusCode, QuestionQueryResult data) result2 = await _httpClient.PostValueAsync<QuestionQueryResult>( "admin/questions/", content1 );
			string questionUId1 = result2.data.Question.Id;

			// Delete Quiz 1
			var result3 = await _httpClient.DeleteAsync( $"/quizzes/{uid1}" );
		}

		public void Dispose() => _db.Cleanup();
	}
}
