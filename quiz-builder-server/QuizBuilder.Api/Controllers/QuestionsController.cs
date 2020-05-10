using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Queries.QuestionQueries;

namespace QuizBuilder.Api.Controllers {
	[ApiController]
	[Route( "[controller]" )]
	public sealed class QuestionsController : ControllerBase {
		private readonly IDispatcher _dispatcher;

		public QuestionsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{id}" )]
		public async Task<ActionResult> GetQuestionById( [FromRoute] GetQuestionByIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpGet]
		public async Task<ActionResult> GetAllQuestions( [FromQuery] GetAllQuestionQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return Ok( result );
		}

		[HttpGet( "group/{groupId}" )]
		public async Task<ActionResult> GetByGroupId( [FromRoute] GetQuestionsByGroupIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpPost]
		public async Task<ActionResult> CreateQuestion( [FromBody] CreateQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof(GetAllQuestions), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> UpdateQuestion( [FromBody] UpdateQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpDelete( "{id}" )]
		public async Task<ActionResult> DeleteQuestion( [FromRoute] DeleteQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}
	}
}
