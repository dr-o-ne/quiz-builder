using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using QuizBuilder.Api;
using QuizBuilder.Data.Dto;
using QuizBuilder.Test.Integration.TestHelpers;
using ServiceStack.OrmLite;
using Xunit;

namespace QuizBuilder.Test.Integration {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class QuizzesControllerTests : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {
				Id = 1,
				UId = "0000000001",
				Name = "Quiz 1",
				IsVisible = true
			},
			new QuizDto {
				Id = 2,
				UId = "0000000002",
				Name = "Quiz 2",
				IsVisible = false
			},
			new QuizDto {
				Id = 1000,
				UId = "0000001000",
				Name = "Quiz To Be Deleted",
				IsVisible = false
			},
			new QuizDto {
				Id = 1001,
				UId = "0000001001",
				Name = "Quiz To Be BulkDeleted",
				IsVisible = true
			},
			new QuizDto {
				Id = 1002,
				UId = "0000001002",
				Name = "Quiz To Be BulkDeleted",
				IsVisible = false
			}

		}.ToImmutableArray();

		private readonly HttpClient _httpClient;
		private readonly TestDatabaseWrapper _db;

		public QuizzesControllerTests( TestApplicationFactory<Startup> factory ) {
			_httpClient = factory.CreateClient();
			IConfiguration config = factory.Services.GetRequiredService<IConfiguration>();
			_db = new TestDatabaseWrapper( config );
			SetupData();
		}

		[Fact]
		public async Task Quiz_GetAll_Success_Test() {
			using var response = await _httpClient.GetAsync( "/quizzes/" );

			Assert.Equal( HttpStatusCode.OK, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_GetById_Success_Test() {
			using var response = await _httpClient.GetAsync( "/quizzes/0000000001" );

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
			var content = JsonConvert.SerializeObject( new { Id = "0000000001", Name = "New Quiz Name" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_Update_Fail_Test() {
			var content = JsonConvert.SerializeObject( new { Id = "0000000100", Name = "New Quiz Name" } );
			using var stringContent = new StringContent( content, Encoding.UTF8, "application/json" );

			using var response = await _httpClient.PutAsync( "/quizzes/", stringContent );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_Success_Test() {
			var response = await _httpClient.DeleteAsync( "/quizzes/0000001000" );
			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_NoItem_Test() {
			using var response = await _httpClient.DeleteAsync( "/quizzes/0000002000" );
			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteBulk_Success_Test() {
			var content = JsonConvert.SerializeObject( new { Ids = new List<string> { "0000001001", "0000001002" } } );

			using var request = new HttpRequestMessage {
				Method = HttpMethod.Delete,
				RequestUri = new Uri( _httpClient.BaseAddress + "quizzes/" ),
				Content = new StringContent( content, Encoding.UTF8, "application/json" )
			};

			using var response = await _httpClient.SendAsync( request );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		private void SetupData() {
			_db.Cleanup();

			using IDbConnection conn = _db.CreateDbConnection();
			conn.Open();

			conn.ExecuteSql( "SET IDENTITY_INSERT dbo.Quiz ON" );
			foreach( var item in QuizData ) {
				conn.Insert( "Quiz", item );
			}
			conn.ExecuteSql( "SET IDENTITY_INSERT dbo.Quiz OFF" );
		}

		public void Dispose() => _db.Cleanup();
	}

}
