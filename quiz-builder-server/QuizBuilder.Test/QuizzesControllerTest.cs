using NUnit.Framework;
using QuizBuilder.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Common.Handlers.Default;

namespace QuizBuilder.Test
{
	public class QuizzesControllerTest
	{
		private QuizzesController _quizzesController;

		[SetUp]
		public void Setup() {
			var services = new ServiceCollection();
			services.AddDispatchers();
			services.AddHandlers();
			services.AddSingleton<QuizzesController>();
			var provider = services.BuildServiceProvider();
			_quizzesController = provider.GetRequiredService<QuizzesController>();
		}

		[Test]
		public async Task TestQuizzesController_GetAll() {
			var actionResult = await _quizzesController.GetAll(new GetAllQuizzesQuery());
			var okResult = actionResult as OkObjectResult;

			Assert.IsNotNull(okResult);

			var result = okResult.Value as AllQuizzesDto;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Quizzes);
			Assert.IsNotEmpty(result.Quizzes);
		}

		[Test]
		public async Task TestQuizzesController_Post()
		{
			var actionResult = await _quizzesController.Post(new CreateQuizCommand());
			var okResult = actionResult as CreatedResult;

			Assert.IsNotNull(okResult);

			var result = okResult.Value as CreateQuizCommandResult;

			Assert.IsNotNull(result);
			Assert.IsTrue(result.Success);
		}
	}
}
