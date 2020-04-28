using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Queries;

namespace QuizBuilder.Api.Controllers {
	[ApiController]
	[Route( "[controller]" )]
	public class QuizzesController : ControllerBase {
		private readonly IDispatcher _dispatcher;

		public QuizzesController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{id}" )]
		public async Task<ActionResult> GetQuizById( [FromRoute] GetQuizByIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpGet]
		public async Task<ActionResult> GetAllQuizzes( [FromQuery] GetAllQuizzesQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return Ok( result );
		}

		[HttpPost]
		public async Task<ActionResult> CreateQuiz( [FromBody] CreateQuizCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof(GetAllQuizzes), result )
				: UnprocessableEntity( result );
			return Created( nameof(GetAllQuizzes), result );
		}

		[HttpPut]
		public async Task<ActionResult> UpdateQuiz( [FromBody] UpdateQuizCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}
	}
}
