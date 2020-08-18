using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[Authorize]
	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class GroupsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public GroupsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateGroupCommand action ) {

			action.SetIdentity( User );

			GroupCommandResult result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateGroupCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete( "{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] string uid ) {

			DeleteGroupCommand action = new DeleteGroupCommand { UId = uid };
			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "reorder" )]
		public async Task<ActionResult> Reorder( [FromBody] ReorderGroupsCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

	}

}
