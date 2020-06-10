using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.ActionResult;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Api.Client.Api.Internal {

	[ApiController]
	[Route( "client/[controller]" )]
	public sealed class AttemptsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public AttemptsController( IDispatcher dispatcher ) => _dispatcher = dispatcher;

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] StartQuizAttemptCommand command ) {

			StartQuizAttemptCommandResult result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( string.Empty ), result )
				: UnprocessableEntity( result );
		}
	}
}
