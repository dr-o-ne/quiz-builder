using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Actions;

namespace QuizBuilder.Api.Controllers {

	[ApiController]
	[Route( "quizzes/{id:alpha}/groups" )]
	public sealed class QuizzesGroupsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuizzesGroupsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet]
		public async Task<ActionResult> Get( [FromRoute] string id ) {
			var query = new GetAllGroupsByQuizQuery {QuizUId = id};
			var result = await _dispatcher.QueryAsync( query );
			throw null;
			//
			//SELECT * FROM dto.Quiz
			//return Ok( result );
		}
	}

}

/*
	quiz
		group
			question
			question
		group
			question
			question



 */
