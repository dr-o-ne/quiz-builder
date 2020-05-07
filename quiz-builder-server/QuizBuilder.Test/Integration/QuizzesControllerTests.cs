using System;
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
	public sealed class QuizzesControllerTests : IClassFixture<TestApplicationFactory<Startup>> {
		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {Id = new Guid( "00000000-0000-0000-0000-000000000001" ), Name = "Quiz 1", IsVisible = true},
			new QuizDto {Id = new Guid( "00000000-0000-0000-0000-000000000002" ), Name = "Quiz 2", IsVisible = false},
			new QuizDto {Id = new Guid( "00000000-0000-0000-0000-000000001000" ), Name = "Quiz To Be Deleted", IsVisible = false},
			new QuizDto {Id = new Guid( "00000000-0000-0000-0000-000000001001" ), Name = "Quiz To Be BulkDeleted", IsVisible = true},
			new QuizDto {Id = new Guid( "00000000-0000-0000-0000-000000001002" ), Name = "Quiz To Be BulkDeleted", IsVisible = false}
		}.ToImmutableArray();

		private readonly HttpClient _httpClient;

		public QuizzesControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			SetupData( factory.DB.ConnectionFactory );
		}

		[Fact]
		public async Task Quiz_GetAll_Success_Test() {
			using var response = await _httpClient.GetAsync( "/quizzes/" );

			Assert.Equal( HttpStatusCode.OK, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_GetById_Success_Test() {
			using var response = await _httpClient.GetAsync( $"/quizzes/{QuizData.First().Id}" );

			Assert.Equal( HttpStatusCode.OK, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Create_Success_Test() {
			var content = JsonConvert.SerializeObject( new {Name = "New Quiz"} );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.Created, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Create_BadRequest_Test() {
			var content = JsonConvert.SerializeObject( new {Unknown = ""} );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.BadRequest, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Update_Success_Test() {
			var content = JsonConvert.SerializeObject( new {QuizData.First().Id, Name = "New Quiz Name"} );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Update_Fail_Test() {
			var content = JsonConvert.SerializeObject( new {Id = Guid.NewGuid(), Name = "New Quiz Name"} );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_Success_Test() {
			var response = await _httpClient.DeleteAsync( $"/quizzes/{QuizData.Skip( 2 ).Take( 1 ).First().Id}" );
			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_Fail_Test() {
			using var response = await _httpClient.DeleteAsync( $"/quizzes/{Guid.NewGuid()}" );
			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteBulk_Success_Test() {
			var content = JsonConvert.SerializeObject( new {Ids = QuizData.Skip( 3 ).Take( 2 ).Select( x => x.Id )} );

			using var request = new HttpRequestMessage {
				Method = HttpMethod.Delete,
				RequestUri = new Uri( _httpClient.BaseAddress + "quizzes/" ),
				Content = new StringContent( content, Encoding.UTF8, "application/json" )
			};

			using var response = await _httpClient.SendAsync( request );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		private static void SetupData( IDbConnectionFactory connectionFactory ) {
			using var connection = connectionFactory.CreateDbConnection();
			connection.Open();
			connection.DropAndCreateTable<QuizDto>( "Quiz" );

			foreach( var item in QuizData ) {
				connection.Insert( "Quiz", item );
			}
		}
	}
}
