using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.WorkflowTests {

	[Trait( "Category", "Integration" )]
	public sealed class ReorderGroupsWorkflow : _WorkflowBase {

		public ReorderGroupsWorkflow( TestApplicationFactory<Startup> factory ) : base( factory ) {
		}

		[Fact]
		public async Task Test() {
			// Create Quiz 1
			(HttpStatusCode statusCode, QuizCommandResult data) result1 = await _apiClient.QuizCreate( new {Name = "Quiz 1"} );
			string uid1 = result1.data.Quiz.Id;

			// Create Group 1
			var result2 = await _apiClient.GroupCreate( new {QuizId = uid1} );
			string uid2 = result2.data.Group.Id;

			// Create Group 2
			var result3 = await _apiClient.GroupCreate( new {QuizId = uid1} );
			string uid3 = result3.data.Group.Id;

			// Create Group 3
			var result4 = await _apiClient.GroupCreate( new {QuizId = uid1} );
			string uid4 = result4.data.Group.Id;

			// Get Quiz, check Groups order
			var result5 = await _apiClient.QuizGet( uid1 );
			var groups = result5.data.Payload.Groups;
			Assert.Equal( uid2, groups[0].Id );
			Assert.Equal( uid3, groups[1].Id );
			Assert.Equal( uid4, groups[2].Id );

			// Reorder
			await _apiClient.GroupReorder( new {quizId = uid1, groupIds = new List<string> {uid4, uid3, uid2}} );

			// Get Quiz, check Groups order
			var result6 = await _apiClient.QuizGet( uid1 );
			var groupsReordered = result6.data.Payload.Groups;
			Assert.Equal( uid4, groupsReordered[0].Id );
			Assert.Equal( uid3, groupsReordered[1].Id );
			Assert.Equal( uid2, groupsReordered[2].Id );
		}

	}
}
