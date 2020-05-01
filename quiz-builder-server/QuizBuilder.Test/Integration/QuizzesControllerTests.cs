using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizBuilder.Api.Controllers;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Extensions;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Mapper.Default;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Queries;
using QuizBuilder.Domain.Queries.QuizQueries;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Repository.Extensions;
using QuizBuilder.Repository.Repository;
using Xunit;

namespace QuizBuilder.Test.Integration {

	public sealed class QuizzesControllerTests {

		private readonly QuizzesController _quizzesController;

		public QuizzesControllerTests() {
			var quizRepositoryMock = new Mock<IGenericRepository<QuizDto>>();
			quizRepositoryMock.Setup( x => x.GetAllAsync() ).ReturnsAsync( new Collection<QuizDto> {new QuizDto(), new QuizDto(), new QuizDto()} );
			quizRepositoryMock.Setup( x => x.AddAsync( It.IsAny<QuizDto>() ) ).ReturnsAsync( 1 );
			quizRepositoryMock.Setup( x => x.UpdateAsync( It.IsAny<QuizDto>() ) ).ReturnsAsync( 1 );
			quizRepositoryMock.Setup( x => x.DeleteAsync( It.IsAny<long>() ) ).ReturnsAsync( 1 );
			quizRepositoryMock.Setup( x => x.GetByIdAsync( It.IsAny<long>() ) ).ReturnsAsync( new QuizDto() {Id = 1} );

			var services = new ServiceCollection();
			services.AddDispatchers();
			services.AddHandlers();
			services.AddMappers();
			services.AddSingleton( typeof(IGenericRepository<QuizDto>), quizRepositoryMock.Object );
			services.AddSingleton<QuizzesController>();
			var provider = services.BuildServiceProvider();
			_quizzesController = provider.GetRequiredService<QuizzesController>();
		}

		[Fact]
		public async Task TestQuizzesController_GetAllQuizzes() {
			var actionResult = await _quizzesController.GetAllQuizzes( new GetAllQuizzesQuery() );
			var okResult = actionResult as OkObjectResult;

			var result = (GetAllQuizzesDto)okResult.Value;

			Assert.NotNull( result.Quizzes );
			Assert.NotEmpty( result.Quizzes );
		}

		[Fact]
		public async Task TestQuizzesController_GetQuizById() {
			var actionResult = await _quizzesController.GetQuizById( new GetQuizByIdQuery() );
			var okResult = actionResult as OkObjectResult;

			var result = (GetQuizByIdDto)okResult.Value;

			Assert.Equal( 1, result.Quiz.Id );
		}

		[Fact]
		public async Task TestQuizzesController_CreateQuiz() {
			var actionResult = await _quizzesController.CreateQuiz( new CreateQuizCommand() );
			var okResult = actionResult as CreatedResult;

			var result = (CommandResult)okResult.Value;

			Assert.True( result.Success );
		}

		[Fact]
		public async Task TestQuizzesController_UpdateQuiz() {
			var actionResult = await _quizzesController.UpdateQuiz( new UpdateQuizCommand() );
			var okResult = actionResult as NoContentResult;

			Assert.NotNull( okResult );
		}

		[Fact]
		public async Task TestQuizzesController_DeleteQuiz() {
			var actionResult = await _quizzesController.DeleteQuiz( new DeleteQuizCommand() );
			var okResult = actionResult as NoContentResult;

			Assert.NotNull( okResult );
		}
	}
}
