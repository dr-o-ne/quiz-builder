using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action;

namespace QuizBuilder.Api.Controllers {

	[ApiController]
	[Route( "[controller]" )]
	public sealed class GroupsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public GroupsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateGroupCommand command ) {
			CreateGroupCommandResult result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateGroupCommand command ) {
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
	}

}

