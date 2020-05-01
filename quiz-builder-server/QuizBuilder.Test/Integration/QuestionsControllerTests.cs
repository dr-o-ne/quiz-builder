using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Repository.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.Data;
using Xunit;

namespace QuizBuilder.Test.Integration {

	public sealed class QuestionsControllerTests : IClassFixture<TestApplicationFactory<Startup>> {

		private static readonly ImmutableArray<QuestionDto> QuizData = new List<QuestionDto> {
			new QuestionDto {
				Id = 1,
				Name = "True/False",
				QuestionTypeId = 1,
				Settings = @"{""TrueChoice"":{""IsCorrect"":false,""Text"":""TrueIncorrect""},""FalseChoice"":{""IsCorrect"":true,""Text"":""FalseCorrect""}}",
				QuestionText = "TrueChoice Question Text"
			},
			new QuestionDto {
				Id = 2,
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
			string result1 = await _httpClient.GetStringAsync( "/questions/1" );
			Assert.Contains( "True/False", result1 );

			string result2 = await _httpClient.GetStringAsync( "/questions/2" );
			Assert.Contains( "MultipleChoice", result2 );
		}

		private static void SetupData( IDbConnectionFactory connectionFactory ) {

			using var connection = connectionFactory.CreateDbConnection();
			connection.Open();
			connection.DropAndCreateTable<QuestionDto>( "Question" );

			foreach( var item in QuizData ) {
				connection.Insert( "Question", item );
			}
		}

	}
}
