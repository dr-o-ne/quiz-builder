using NUnit.Framework;
using QuizBuilder.Api;
using QuizBuilder.Api.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace QuizBuilder.Test
{
	public class QuizzesControllerTest
	{
		private QuizzesController _quizzesController;

		[SetUp]
		public void Setup() {
			_quizzesController = new QuizzesController(null);
		}

		[Test]
		public void TestQuizzesController() {
			IEnumerable<Quiz> result = _quizzesController.Get();

			Assert.IsNotNull(result);
			Assert.IsTrue(result.Any());
		}
	}
}