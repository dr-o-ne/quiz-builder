using System.Collections.ObjectModel;
using QuizBuilder.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Common.Handlers.Default;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Mapper.Default;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Repository;
using Xunit;

namespace QuizBuilder.Test {

	public sealed class QuizzesControllerTests {

		private readonly QuizzesController _quizzesController;

		public QuizzesControllerTests() {
			var quizRepositoryMock = new Mock<IGenericRepository<QuizDto>>();
			quizRepositoryMock.Setup( x => x.GetAllAsync() ).ReturnsAsync( new Collection<QuizDto> {new QuizDto(), new QuizDto(), new QuizDto()} );
			quizRepositoryMock.Setup( x => x.AddAsync( It.IsAny<QuizDto>() ) ).ReturnsAsync( 1 );
			quizRepositoryMock.Setup( x => x.GetByIdAsync( It.IsAny<long>() ) ).ReturnsAsync( new QuizDto {Id = 1} );

			var services = new ServiceCollection();
			services.AddDispatchers();
			services.AddHandlers();
			services.AddSingleton( typeof(IQuizMapper), new QuizMapper() );
			services.AddSingleton( typeof(IGenericRepository<QuizDto>), quizRepositoryMock.Object );
			services.AddSingleton<QuizzesController>();
			var provider = services.BuildServiceProvider();
			_quizzesController = provider.GetRequiredService<QuizzesController>();
		}

		[Fact]
		public async Task TestQuizzesController_GetAll() {
			var actionResult = await _quizzesController.GetAll( new GetAllQuizzesQuery() );
			var okResult = actionResult as OkObjectResult;

			var result = (AllQuizzesDto)okResult.Value;

			Assert.NotNull( result.Quizzes );
			Assert.NotEmpty( result.Quizzes );
		}

		[Fact]
		public async Task TestQuizzesController_GetById() {
			var actionResult = await _quizzesController.GetById( new GetQuizByIdQuery() );
			var okResult = actionResult as OkObjectResult;

			var result = (GetQuizByIdDto)okResult.Value;

			Assert.Equal( 1, result.Id );
		}

		[Fact]
		public async Task TestQuizzesController_Post() {
			var actionResult = await _quizzesController.Post( new CreateQuizCommand() );
			var okResult = actionResult as CreatedResult;

			var result = (CreateQuizCommandResult)okResult.Value;

			Assert.True( result.Success );
		}
	}
}
