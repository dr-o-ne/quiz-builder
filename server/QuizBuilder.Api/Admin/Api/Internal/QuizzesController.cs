using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Domain.Action.Admin.Action;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[Authorize]
	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class QuizzesController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuizzesController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{uid}" )]
		public async Task<ActionResult> Get( [FromRoute] GetQuizByIdQuery action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.QueryAsync( action );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpGet]
		public async Task<ActionResult> GetAll( [FromQuery] GetAllQuizzesQuery action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.QueryAsync( action );

			return Ok( result );
		}

		[HttpDelete( "{quizUId}/questions/{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] DeleteQuestionCommand action ) {

			action.SetIdentity( User );

			CommandResult result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateQuizCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateQuizCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete( "{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] DeleteQuizCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete]
		public async Task<ActionResult> Delete( [FromBody] DeleteQuizzesCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}
	}
}
