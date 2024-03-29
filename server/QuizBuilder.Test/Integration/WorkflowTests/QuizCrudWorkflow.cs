﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	public sealed class QuizCrudWorkflow : _WorkflowBase {

		public QuizCrudWorkflow( TestApplicationFactory<Startup> factory ) : base( factory ) {
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, CommandResult<QuizViewModel> data) result1 = await _apiClient.QuizCreate( new { Name = "Quiz 1" } );
			string uid1 = result1.data.Payload.Id;

			// Create Quiz 2
			(HttpStatusCode statusCode, CommandResult<QuizViewModel> data) result2 = await _apiClient.QuizCreate( new { Name = "Quiz 2" } );
			string uid2 = result2.data.Payload.Id;

			// Create Quiz 3
			(HttpStatusCode statusCode, CommandResult<QuizViewModel> data) result3 = await _apiClient.QuizCreate( new { Name = "Quiz 3" } );
			string uid3 = result3.data.Payload.Id;
			
			// Get All Quizzes
			(HttpStatusCode statusCode, CommandResult<ImmutableList<QuizViewModel>> data) result4 = await _apiClient.QuizGetAll();
			Assert.Equal( 3, result4.data.Payload.Count );
			Assert.Equal( uid1, result4.data.Payload[0].Id );
			Assert.Equal( uid2, result4.data.Payload[1].Id );
			Assert.Equal( uid3, result4.data.Payload[2].Id );
			Assert.Equal( "Quiz 1", result4.data.Payload[0].Name );
			Assert.Equal( "Quiz 2", result4.data.Payload[1].Name );
			Assert.Equal( "Quiz 3", result4.data.Payload[2].Name );
			Assert.False( result4.data.Payload[0].IsEnabled );
			Assert.False( result4.data.Payload[1].IsEnabled );
			Assert.False( result4.data.Payload[2].IsEnabled );

			// Update Quiz 1
			await _apiClient.QuizUpdate( new { Id = uid1, Name = "Quiz 1 New Name" } );

			// Update Quiz 3
			await _apiClient.QuizUpdate( new { Id = uid3, Name = "Quiz 3", IsEnabled = true } );

			// Get All Quizzes
			(HttpStatusCode statusCode, CommandResult<ImmutableList<QuizViewModel>> data) result7 = await _apiClient.QuizGetAll();
			Assert.Equal( 3, result7.data.Payload.Count );
			Assert.Equal( "Quiz 1 New Name", result7.data.Payload[0].Name );
			Assert.True( result7.data.Payload[2].IsEnabled );

			// Delete Quiz 1
			using HttpResponseMessage response8 = await _apiClient.QuizDelete( uid1 );

			// Get All Quizzes
			(HttpStatusCode statusCode, CommandResult<ImmutableList<QuizViewModel>> data) result9 = await _apiClient.QuizGetAll();
			Assert.Equal( 2, result9.data.Payload.Count );

			using HttpResponseMessage response = await _apiClient.QuizDelete( new List<string> { uid2, uid3 } );

			// Final Check
			(HttpStatusCode statusCode, CommandResult<ImmutableList<QuizViewModel>> data) result11 = await _apiClient.QuizGetAll();
			Assert.Empty( result11.data.Payload );
		}

	}

}
