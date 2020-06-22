using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.ControllerTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class QuestionsControllerTests : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {
				Id = 1,
				UId = "1000000001",
				Name = "Quiz 1",
				IsEnabled = true
			}
		}.ToImmutableArray();

		private static readonly ImmutableArray<QuestionDto> QuestionData = new List<QuestionDto> {
			new QuestionDto {
				Id = 1,
				UId = "000000001",
				Name = "True/False",
				TypeId = 1,
				Settings = @"{""TrueChoice"":{""IsCorrect"":false,""Text"":""TrueIncorrect""},""FalseChoice"":{""IsCorrect"":true,""Text"":""FalseCorrect""}}",
				Text = "TrueChoice Question Text"
			},
			new QuestionDto {
				Id = 2,
				UId = "000000002",
				Name = "MultipleChoice",
				TypeId = 2,
				Settings = @"{""Choices"":[{""IsCorrect"":true,""Text"":""Choice1""},{""IsCorrect"":false,""Text"":""Choice2""},{""IsCorrect"":false,""Text"":""Choice3""}],""Randomize"":true}",
				Text = "MultiChoice Question Text"
			}
		}.ToImmutableArray();

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public QuestionsControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			_db = factory.GetTestDatabaseWrapper();
			SetupData();
		}

		[Fact]
		public async Task Questions_GetById_OK_Test() {

			(HttpStatusCode statusCode, QuestionQueryResult data) result = await _httpClient.GetValueAsync<QuestionQueryResult>( "admin/questions/000000001" );

			Assert.Equal( HttpStatusCode.OK, result.statusCode );
			Assert.Equal( "000000001", result.data.Question.Id );
			Assert.Equal( "True/False", result.data.Question.Name );
		}

		[Fact]
		public async Task Questions_GetById_NoContent_Test() {

			(HttpStatusCode statusCode, QuestionQueryResult data) result = await _httpClient.GetValueAsync<QuestionQueryResult>( "admin/questions/001230001" );

			Assert.Equal( HttpStatusCode.NoContent, result.statusCode );
		}

		[Fact]
		public async Task Question_Create_Created_Test() {

			var content = new {
				QuizId = "1000000001",
				Name = "Question Name",
				Text = "Question Text",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};

			(HttpStatusCode statusCode, QuestionCommandResult data) result = await _httpClient.PostValueAsync<QuestionCommandResult>( "admin/questions/", content );

			Assert.Equal( HttpStatusCode.Created, result.statusCode );
			Assert.False( string.IsNullOrWhiteSpace( result.data.Question.Id ) );
			Assert.Equal( "Question Name", result.data.Question.Name );
			Assert.Equal( "Question Text", result.data.Question.Text );
		}

		[Fact]
		public async Task Question_Create_BadRequest_Test() {

			(HttpStatusCode statusCode, QuestionCommandResult data) result = await _httpClient.PostValueAsync<QuestionCommandResult>( "admin/questions/", new { Unknown = "" } );

			Assert.Equal( HttpStatusCode.BadRequest, result.statusCode );
		}

		[Fact]
		public async Task Question_Update_NoContent_Test() {

			var content = new {
				Id = "000000001",
				Name = "Question Name",
				Text = "Question Text",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			};

			(HttpStatusCode statusCode, QuestionQueryResult data) result = await _httpClient.PutValueAsync<QuestionQueryResult>( "admin/questions/", content );

			Assert.Equal( HttpStatusCode.NoContent, result.statusCode );
		}

		private void SetupData() {
			_db.Cleanup();
			_db.Insert( "Quiz", QuizData );
			_db.Insert( "Question", QuestionData );
		}

		public void Dispose() => _db.Cleanup();
	}
}
