using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QuizBuilder.Common.Dispatchers;
using QuizBuilder.Common.Handlers.Default;

namespace QuizBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public QuizzesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] GetQuizByIdQuery query)
        {
            var result = await _dispatcher.QueryAsync(query);

            return result is null
                ? (ActionResult) NoContent()
                : Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllQuizzesQuery query)
        {
            var result = await _dispatcher.QueryAsync(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateQuizCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Created(nameof(GetAll), result);
        }
    }
}
