using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace quiz_builder_api.Controllers
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
