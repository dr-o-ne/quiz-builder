using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class QuestionsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuestionsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{uid}" )]
		public async Task<ActionResult> Get( [FromRoute] GetQuestionByIdQuery query ) {
			QuestionQueryResult result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "reorder" )]
		public async Task<ActionResult> Reorder( [FromBody] ReorderQuestionsCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "move" )]
		public async Task<ActionResult> Move( [FromBody] MoveQuestionCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.Success
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}


	}
}
