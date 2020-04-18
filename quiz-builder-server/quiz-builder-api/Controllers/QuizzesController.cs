using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace QuizBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(ILogger<QuizzesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Quiz> Get()
        {
            return new Quiz[]
            {
                new Quiz()
                {
                    Id = 1,
                    Name = "First Quiz"
                },
                new Quiz()
                {
                    Id = 2,
                    Name = "Second Quiz"
                },
                new Quiz()
                {
                    Id = 3,
                    Name = "Third Quiz"
                },
            };
        }
    }
}
