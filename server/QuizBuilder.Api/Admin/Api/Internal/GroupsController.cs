﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class GroupsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public GroupsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateGroupCommand command ) {
			GroupCommandResult result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut( "rename" )]
		public async Task<ActionResult> Rename( [FromBody] UpdateGroupNameCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete( "{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] DeleteGroupCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "reorder" )]
		public async Task<ActionResult> Reorder( [FromBody] ReorderGroupsCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

	}

}

