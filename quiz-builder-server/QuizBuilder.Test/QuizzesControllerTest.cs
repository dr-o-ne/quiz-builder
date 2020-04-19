using System.Collections.ObjectModel;
using NUnit.Framework;
using QuizBuilder.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Common.Handlers.Default;
using QuizBuilder.Model.Model.Default;
using QuizBuilder.Repository.Repository;

namespace QuizBuilder.Test
{
    public class QuizzesControllerTest
    {
        private QuizzesController _quizzesController;
        private Mock<IQuizRepository> _quizRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _quizRepositoryMock = new Mock<IQuizRepository>();
            _quizRepositoryMock.Setup(x => x.GetAll()).Returns(
                new Collection<Quiz>() {new Quiz(), new Quiz(), new Quiz()});
            _quizRepositoryMock.Setup(x => x.Add(It.IsAny<Quiz>())).Returns(new Quiz() {Id = 1});
            _quizRepositoryMock.Setup(x => x.GetById(It.IsAny<long>())).Returns(new Quiz() {Id = 1});

            var services = new ServiceCollection();
            services.AddDispatchers();
            services.AddHandlers();
            services.AddSingleton(typeof(IQuizRepository), _quizRepositoryMock.Object);
            services.AddSingleton<QuizzesController>();
            var provider = services.BuildServiceProvider();
            _quizzesController = provider.GetRequiredService<QuizzesController>();
        }

        [Test]
        public async Task TestQuizzesController_GetAll()
        {
            var actionResult = await _quizzesController.GetAll(new GetAllQuizzesQuery());
            var okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);

            var result = okResult.Value as AllQuizzesDto;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Quizzes);
            Assert.IsNotEmpty(result.Quizzes);
        }

        [Test]
        public async Task TestQuizzesController_GetById()
        {
            var actionResult = await _quizzesController.GetById(new GetQuizByIdQuery());
            var okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);

            var result = okResult.Value as GetQuizByIdDto;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
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
