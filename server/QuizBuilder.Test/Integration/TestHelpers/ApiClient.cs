using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizBuilder.Common;
using QuizBuilder.Common.CQRS.Actions.Default;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Action.Client.Action;
using QuizBuilder.Domain.Action.Client.ActionResult;

namespace QuizBuilder.Test.Integration.TestHelpers {

	public sealed class ApiClient {

		private readonly HttpClient _httpClient;

		public ApiClient( HttpClient httpClient ) {
			_httpClient = httpClient;
		}

		#region Quiz

		public Task<(HttpStatusCode statusCode, CommandResult<QuizViewModel> data)> QuizGet( string uid ) =>
			_httpClient.GetValueAsync<CommandResult<QuizViewModel>>( "admin/quizzes/" + uid );

		public Task<(HttpStatusCode statusCode, CommandResult<ImmutableList<QuizViewModel>> data)> QuizGetAll() =>
			_httpClient.GetValueAsync<CommandResult<ImmutableList<QuizViewModel>>>( "admin/quizzes/" );

		public Task<(HttpStatusCode statusCode, CommandResult<QuizViewModel> data)> QuizCreate( object content ) =>
			_httpClient.PostValueAsync<CommandResult<QuizViewModel>>( "admin/quizzes/", ToCommand<CreateQuizCommand>( content ) );

		public Task<(HttpStatusCode statusCode, CommandResult<QuizViewModel> data)> QuizUpdate( object content ) =>
			_httpClient.PutValueAsync<CommandResult<QuizViewModel>>( "admin/quizzes/", ToCommand<UpdateQuizCommand>( content ) );

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

		public Task<(HttpStatusCode statusCode, GroupCommandResult data)> GroupReorder( object content ) =>
			_httpClient.PutValueAsync<GroupCommandResult>( "admin/groups/reorder/", ToCommand<ReorderGroupsCommand>( content ) );

		public Task<HttpResponseMessage> GroupDelete( string uid ) =>
			_httpClient.DeleteAsync( "admin/groups/" + uid );

		#endregion

		#region Question

		public Task<(HttpStatusCode statusCode, CommandResult<QuestionViewModel> data)> QuestionGet( string uid ) =>
			_httpClient.GetValueAsync<CommandResult<QuestionViewModel>>( "admin/questions/" + uid );

		public Task<(HttpStatusCode statusCode, QuestionCommandResult data)> QuestionCreate( object content ) =>
			_httpClient.PostValueAsync<QuestionCommandResult>( "admin/questions/", ToCommand<CreateQuestionCommand>( content ) );

		#endregion

		#region Attempt

		public Task<(HttpStatusCode statusCode, QuizAttemptInfo data)> AttemptStart( string uid ) =>
			_httpClient.PostValueAsync<QuizAttemptInfo>( "client/attempts/", ToCommand<StartQuizAttemptCommand>( new {QuizId = uid} ) );

		#endregion

		private static T ToCommand<T>( object content ) {
			string json = JsonSerializer.Serialize( content, Consts.JsonSerializerOptions );
			return JsonSerializer.Deserialize<T>( json, Consts.JsonSerializerOptions );
		}

	}
}
