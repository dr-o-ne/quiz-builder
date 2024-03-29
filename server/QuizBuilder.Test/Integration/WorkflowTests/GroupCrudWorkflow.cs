﻿using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	public sealed class GroupCrudWorkflow : _WorkflowBase {

		public GroupCrudWorkflow( TestApplicationFactory<Startup> factory ) : base( factory ) {
		}

		[Fact]
		public async Task Test() {

			// Create Quiz 1
			(HttpStatusCode statusCode, CommandResult<QuizViewModel> data) result1 = await _apiClient.QuizCreate( new {Name = "Quiz 1"} );
			string uid1 = result1.data.Payload.Id;

			// Create Group 1
			var result2 = await _apiClient.GroupCreate( new {QuizId = uid1} );
			string uid2 = result2.data.Group.Id;

			// Get Quiz, check Groups
			var result3 = await _apiClient.QuizGet( uid1 );
			var groups = result3.data.Payload.Groups;
			Assert.Equal( uid2, groups[0].Id );
			Assert.Equal( "Group 1", groups[0].Name );

			// Delete Group Name
			await _apiClient.GroupDelete( uid2 );

			// Get Quiz, check Groups
			var result4 = await _apiClient.QuizGet( uid1 );
			Assert.Empty( result4.data.Payload.Groups );

		}
	}
}
