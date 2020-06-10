using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[Obsolete]
	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class AttemptsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public AttemptsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] StartQuizAttemptCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] EndQuizAttemptCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Ok( result )
				: UnprocessableEntity( result );
		}

	}

}
