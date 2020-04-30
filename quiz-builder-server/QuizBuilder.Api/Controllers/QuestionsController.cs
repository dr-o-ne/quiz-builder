using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Domain.Queries;

namespace QuizBuilder.Api.Controllers {

	[ApiController]
	[Route( "[controller]" )]
	public sealed class QuestionsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuestionsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{id}" )]
		public async Task<ActionResult> GetById( [FromRoute] GetQuestionByIdQuery query ) {
			var result = await _dispatcher.QueryAsync( query );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}
	}

}
