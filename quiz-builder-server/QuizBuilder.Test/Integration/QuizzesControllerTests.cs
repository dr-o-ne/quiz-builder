using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
			new QuizDto {
				Id = 1,
				Name = "Quiz 1",
				IsVisible = true
			},
			new QuizDto {
				Id = 2,
				Name = "Quiz 2",
				IsVisible = false
			},
			new QuizDto {
				Id = 1000,
				Name = "Quiz To Be Deleted",
				IsVisible = false
			},
			new QuizDto {
				Id = 1001,
				Name = "Quiz To Be BulkDeleted",
				IsVisible = true
			},
			new QuizDto {
				Id = 1002,
				Name = "Quiz To Be BulkDeleted",
				IsVisible = false
			}

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
			using var response = await _httpClient.GetAsync( "/quizzes/1" );

			Assert.Equal( HttpStatusCode.OK, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Create_Success_Test() {
			var content = JsonConvert.SerializeObject( new { Name = "New Quiz" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.Created, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Create_BadRequest_Test() {
			var content = JsonConvert.SerializeObject( new { Unknown = "" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PostAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.BadRequest, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Update_Success_Test() {
			var content = JsonConvert.SerializeObject( new { Id = 1 , Name = "New Quiz Name" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Update_Fail_Test() {
			var content = JsonConvert.SerializeObject( new { Id = 100, Name = "New Quiz Name" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_Success_Test() {
			var response = await _httpClient.DeleteAsync( "/quizzes/1000" );
			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_Fail_Test() {
			using var response = await _httpClient.DeleteAsync( "/quizzes/2000" );
			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteBulk_Success_Test() {
			var content = JsonConvert.SerializeObject( new {Ids = new List<long> {1001, 1002}} );

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
