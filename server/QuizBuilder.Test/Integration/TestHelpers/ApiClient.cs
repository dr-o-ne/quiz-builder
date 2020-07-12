using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Utils;

namespace QuizBuilder.Test.Integration.TestHelpers {

	internal sealed class ApiClient {

		private readonly HttpClient _httpClient;

		public ApiClient( HttpClient httpClient ) {
			_httpClient = httpClient;
		}

		#region Quiz

		public Task<(HttpStatusCode statusCode, QuizQueryResult data)> QuizGet( string uid ) =>
			_httpClient.GetValueAsync<QuizQueryResult>( "admin/quizzes/" + uid );

		public Task<(HttpStatusCode statusCode, QuizzesQueryResult data)> QuizGetAll() =>
			_httpClient.GetValueAsync<QuizzesQueryResult>( "admin/quizzes/" );

		public Task<(HttpStatusCode statusCode, QuizCommandResult data)> QuizCreate( object content ) =>
			_httpClient.PostValueAsync<QuizCommandResult>( "admin/quizzes/", ToCommand<CreateQuizCommand>( content ) );

		public Task<(HttpStatusCode statusCode, QuizCommandResult data)> QuizUpdate( object content ) =>
			_httpClient.PutValueAsync<QuizCommandResult>( "admin/quizzes/", ToCommand<UpdateQuizCommand>( content ) );

		public Task<HttpResponseMessage> QuizDelete( string uid ) =>
			_httpClient.DeleteAsync( "admin/quizzes/" + uid );

		public async Task<HttpResponseMessage> QuizDelete( List<string> uids ) {
			string content = JsonSerializer.Serialize( new { Ids = uids } );
			using var request = new HttpRequestMessage {
				Method = HttpMethod.Delete,
				RequestUri = new Uri( _httpClient.BaseAddress + "admin/quizzes/" ),
				Content = new StringContent( content, Encoding.UTF8, "application/json" )
			};
			return await _httpClient.SendAsync( request );
		}

		#endregion

		#region Group

		public Task<(HttpStatusCode statusCode, GroupCommandResult data)> GroupCreate( object content ) =>
			_httpClient.PostValueAsync<GroupCommandResult>( "admin/groups/", ToCommand<CreateGroupCommand>( content ) );

		#endregion

		#region Question

		

		#endregion

		private static T ToCommand<T>( object content ) {
			string json = JsonSerializer.Serialize( content, Consts.JsonSerializerOptions );
			return JsonSerializer.Deserialize<T>( json, Consts.JsonSerializerOptions );
		}

	}
}
