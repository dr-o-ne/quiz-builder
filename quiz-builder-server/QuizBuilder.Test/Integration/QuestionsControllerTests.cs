using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizBuilder.Api;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.Data;
using Xunit;

namespace QuizBuilder.Test.Integration {

	public sealed class QuestionsControllerTests : IClassFixture<TestApplicationFactory<Startup>> {

		private static readonly ImmutableArray<QuestionDto> QuestionData = new List<QuestionDto> {
			new QuestionDto {
				Id = 1,
				UId = "000000001",
				Name = "True/False",
				QuestionTypeId = 1,
				Settings = @"{""TrueChoice"":{""IsCorrect"":false,""Text"":""TrueIncorrect""},""FalseChoice"":{""IsCorrect"":true,""Text"":""FalseCorrect""}}",
				QuestionText = "TrueChoice Question Text"
			},
			new QuestionDto {
				Id = 2,
				UId = "000000002",
				Name = "MultipleChoice",
				QuestionTypeId = 2,
				Settings = @"{""Choices"":[{""IsCorrect"":true,""Text"":""Choice1""},{""IsCorrect"":false,""Text"":""Choice2""},{""IsCorrect"":false,""Text"":""Choice3""}],""Randomize"":true}",
				QuestionText = "MultiChoice Question Text"
			}
		}.ToImmutableArray();

		private readonly HttpClient _httpClient;

		public QuestionsControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			SetupData( factory.DB.ConnectionFactory );
		}

		[Fact]
		public async Task Questions_GetById_Success_Test() {
			string result1 = await _httpClient.GetStringAsync( $"/questions/{QuestionData.First().UId}" );
			Assert.Contains( "True/False", result1 );

			string result2 = await _httpClient.GetStringAsync( $"/questions/{QuestionData.Last().UId}" );
			Assert.Contains( "MultipleChoice", result2 );
		}

		[Fact]
		public async Task Question_Create_Success_Test() {
			var content = JsonConvert.SerializeObject( new {
				Name = "Question Name",
				Text = "Question Text",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			} );

			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/questions/", stringContent );

			Assert.Equal( HttpStatusCode.Created, response.StatusCode );
		}

		[Fact]
		public async Task Question_Create_BadRequest_Test() {
			var content = JsonConvert.SerializeObject( new { Unknown = "" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/questions/", stringContent );

			Assert.Equal( HttpStatusCode.InternalServerError, response.StatusCode );
		}

		[Fact]
		public async Task Question_Update_Success_Test() {
			var content = JsonConvert.SerializeObject( new {
				Id = QuestionData.First().UId,
				Name = "Question Name",
				Text = "Question Text",
				Type = 1,
				Settings = "{\"choicesDisplayType\":1,\"choicesEnumerationType\":2}",
				Choices = "[{\"isCorrect\":true,\"text\":\"Choice 1\"},{\"isCorrect\":false,\"text\":\"Choice 2\"}]"
			} );

			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/questions/", stringContent );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Question_DeleteById_Success_Test() {
			var response = await _httpClient.DeleteAsync( "/questions/000000001" );
			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Question_DeleteById_Fail_Test() {
			using var response = await _httpClient.DeleteAsync( "/questions/000000011" );
			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		private static void SetupData( IDbConnectionFactory connectionFactory ) {

			using var connection = connectionFactory.CreateDbConnection();
			connection.Open();
			connection.DropAndCreateTable<QuestionDto>( "Question" );

			foreach( var item in QuestionData ) {
				connection.Insert( "Question", item );
			}
		}

	}
}
