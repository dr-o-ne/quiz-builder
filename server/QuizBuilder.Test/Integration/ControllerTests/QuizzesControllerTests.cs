﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Threading.Tasks;
using QuizBuilder.Api;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Test.Integration.TestHelpers;
using Xunit;

namespace QuizBuilder.Test.Integration.ControllerTests {

	[Trait( "Category", "Integration" )]
	[Collection( "DB" )]
	public sealed class QuizzesControllerTests : IClassFixture<TestApplicationFactory<Startup>>, IDisposable {

		private static readonly ImmutableArray<QuizDto> QuizData = new List<QuizDto> {
			new QuizDto {Id = 1, UId = "0000000001", Name = "Quiz 1", IsEnabled = true},
			new QuizDto {Id = 2, UId = "0000000002", Name = "Quiz 2", IsEnabled = false},
			new QuizDto {Id = 1000, UId = "0000001000", Name = "Quiz To Be Deleted", IsEnabled = false},
			new QuizDto {Id = 1001, UId = "0000001001", Name = "Quiz To Be BulkDeleted", IsEnabled = true},
			new QuizDto {Id = 1002, UId = "0000001002", Name = "Quiz To Be BulkDeleted", IsEnabled = false}
		}.ToImmutableArray();

		private readonly ApiClient _apiClient;
		private readonly TestDatabaseWrapper _db;

		public QuizzesControllerTests( TestApplicationFactory<Startup> factory ) {
			_apiClient = new ApiClient( factory.CreateClient() );
			_db = factory.GetTestDatabaseWrapper();
			SetupData();
		}

		[Fact]
		public async Task Quiz_GetById_OK_Test() {
			(HttpStatusCode statusCode, QuizQueryResult data) result = await _apiClient.QuizGet( "0000000001" );

			Assert.Equal( HttpStatusCode.OK, result.statusCode );
			Assert.Equal( "0000000001", result.data.Quiz.Id );
			Assert.Equal( "Quiz 1", result.data.Quiz.Name );
			Assert.True( result.data.Quiz.IsEnabled );
		}

		[Fact]
		public async Task Quiz_GetById_NoContent_Test() {
			(HttpStatusCode statusCode, QuizQueryResult data) result = await _apiClient.QuizGet( "00" );

			Assert.Equal( HttpStatusCode.NoContent, result.statusCode );
		}

		[Fact]
		public async Task Quiz_GetAll_OK_Test() {
			(HttpStatusCode statusCode, QuizzesQueryResult data) result = await _apiClient.QuizGetAll();

			Assert.Equal( HttpStatusCode.OK, result.statusCode );
			Assert.Equal( 5, result.data.Quizzes.Count );
			Assert.Equal( "0000000001", result.data.Quizzes[0].Id );
			Assert.Equal( "0000000002", result.data.Quizzes[1].Id );
			Assert.Equal( "0000001000", result.data.Quizzes[2].Id );
			Assert.Equal( "0000001001", result.data.Quizzes[3].Id );
			Assert.Equal( "0000001002", result.data.Quizzes[4].Id );
		}

		[Fact]
		public async Task Quiz_Create_Created_Test() {
			(HttpStatusCode statusCode, QuizCommandResult data) result = await _apiClient.QuizCreate( new {Name = "New Quiz"} );

			Assert.Equal( HttpStatusCode.Created, result.statusCode );
			Assert.False( string.IsNullOrWhiteSpace( result.data.Quiz.Id ) );
			Assert.Equal( "New Quiz", result.data.Quiz.Name );
			Assert.False( result.data.Quiz.IsEnabled );
		}

		[Fact]
		public async Task Quiz_Create_BadRequest_Test() {
			(HttpStatusCode statusCode, QuizCommandResult data) result = await _apiClient.QuizCreate( new {Unknown = ""} );

			Assert.Equal( HttpStatusCode.BadRequest, result.statusCode );
		}

		[Fact]
		public async Task Quiz_Update_Success_Test() {
			(HttpStatusCode statusCode, QuizCommandResult data) result = await _apiClient.QuizUpdate( new {Id = "0000000001", Name = "New Quiz Name"} );

			Assert.Equal( HttpStatusCode.NoContent, result.statusCode );
		}

		[Fact]
		public async Task Quiz_Update_Fail_Test() {
			(HttpStatusCode statusCode, QuizCommandResult data) result = await _apiClient.QuizUpdate( new {Id = "0000000100", Name = "New Quiz Name"} );

			Assert.Equal( HttpStatusCode.UnprocessableEntity, result.statusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_NoContent_Test() {
			var response = await _apiClient.QuizDelete( "0000001000" );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteById_NoItem_NoContent_Test() {
			using var response = await _apiClient.QuizDelete( "0000002000" );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		[Fact]
		public async Task Quiz_DeleteBulk_NoContent_Test() {
			using var response = await _apiClient.QuizDelete( new List<string> { "0000001001", "0000001002" } );

			Assert.Equal( HttpStatusCode.NoContent, response.StatusCode );
		}

		private void SetupData() {
			_db.Cleanup();
			_db.Insert( "Quiz", QuizData );
		}

		public void Dispose() => _db.Cleanup();
	}
}
