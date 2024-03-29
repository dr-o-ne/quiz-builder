﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Domain.Action.Client.Action;
using StartQuizAttemptCommand = QuizBuilder.Domain.Action.Client.Action.StartQuizAttemptCommand;

namespace QuizBuilder.Api.Client.Api.Internal {

	[ApiController]
	[Route( "client/[controller]" )]
	public sealed class AttemptsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public AttemptsController( IDispatcher dispatcher ) => _dispatcher = dispatcher;

		[HttpGet( "{uid}" )]
		public async Task<ActionResult> Get( [FromRoute] string uid ) {

			var action = new GetQuizInfoAction {QuizUId = uid};
			var result = await _dispatcher.QueryAsync( action );

			return !result.IsSuccess
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpGet( "{uid}/result" )]
		public async Task<ActionResult> GetResult( [FromRoute] string uid ) {

			var action = new GetAttemptFeedbackInfoAction {AttemptUId = uid};
			var result = await _dispatcher.QueryAsync( action );

			return !result.IsSuccess
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] StartQuizAttemptCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.IsSuccess
				? (ActionResult)Created( nameof( string.Empty ), result )
				: UnprocessableEntity( string.Empty );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] EndQuizAttemptCommand command ) {
			var result = await _dispatcher.SendAsync( command );

			return result.IsSuccess
				? (ActionResult)Ok( result )
				: UnprocessableEntity( string.Empty );
		}

	}
}
