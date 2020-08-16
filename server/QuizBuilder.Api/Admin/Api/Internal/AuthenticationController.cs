using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class AuthenticationController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public AuthenticationController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpPost( "register" )]
		public async Task<ActionResult> Register( [FromBody] RegisterUserCommand userCommand ) {

			var result = await _dispatcher.SendAsync( userCommand );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Register ), result )
				: UnprocessableEntity( result );
		}

		[HttpPost( "login" )]
		public async Task<ActionResult> Login( LoginUserCommand command ) {

			var result = await _dispatcher.SendAsync( command );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Login ), result )
				: UnprocessableEntity( result );
		}

	}

}
