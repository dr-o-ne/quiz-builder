using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action;
using QuizBuilder.Domain.Action.Action;

namespace QuizBuilder.Api.Controllers {

	[ApiController]
	[Route( "[controller]" )]
	public sealed class QuizzesController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuizzesController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{uid}" )]
		public async Task<ActionResult> Get( [FromRoute] GetQuizByIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpGet]
		public async Task<ActionResult> GetAll( [FromQuery] GetAllQuizzesQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return Ok( result );
		}

		//TODO: test
		[HttpGet("{quizUId}/questions")]
		public async Task<ActionResult> GetAll( [FromRoute] GetQuestionsByQuizIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return Ok( result );
		}

		[HttpDelete( "{quizUId}/questions/{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] DeleteQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateQuizCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateQuizCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete( "{uid}" )]
		public async Task<ActionResult> Delete( [FromRoute] DeleteQuizCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete]
		public async Task<ActionResult> Delete( [FromBody] DeleteQuizzesCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}
	}
}
