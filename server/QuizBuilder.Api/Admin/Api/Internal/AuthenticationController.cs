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

		[HttpPost]
		public async Task<ActionResult> SignUp( [FromBody] SignUpCommand command ) {

			var result = await _dispatcher.SendAsync( command );

			return result.IsSuccess
				? (ActionResult)Created( nameof( SignUp ), result )
				: UnprocessableEntity( result );
		}

		[HttpPost( "authenticate" )]
		public async Task<ActionResult> Authenticate( AuthenticateUserCommand command ) {

			var result = await _dispatcher.SendAsync( command );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Authenticate ), result )
				: UnprocessableEntity( result );
		}

	}

}
