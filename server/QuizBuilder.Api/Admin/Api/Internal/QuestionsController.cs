﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.CQRS.Dispatchers;
using QuizBuilder.Common.Extensions;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Api.Admin.Api.Internal {

	[Authorize]
	[ApiController]
	[Route( "admin/[controller]" )]
	public sealed class QuestionsController : ControllerBase {

		private readonly IDispatcher _dispatcher;

		public QuestionsController( IDispatcher dispatcher ) {
			_dispatcher = dispatcher;
		}

		[HttpGet( "{uid}" )]
		public async Task<ActionResult> Get( [FromRoute] string uid ) {

			var action = new GetQuestionByIdQuery { UId = uid };
			action.SetIdentity( User );

			QuestionQueryResult result = await _dispatcher.QueryAsync( action );

			return result is null
				? (ActionResult)NoContent()
				: Ok( result );
		}

		[HttpPost]
		public async Task<ActionResult> Create( [FromBody] CreateQuestionCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)Created( nameof( Create ), result )
				: UnprocessableEntity( result );
		}

		[HttpPut]
		public async Task<ActionResult> Update( [FromBody] UpdateQuestionCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "reorder" )]
		public async Task<ActionResult> Reorder( [FromBody] ReorderQuestionsCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}

		[HttpPut( "move" )]
		public async Task<ActionResult> Move( [FromBody] MoveQuestionCommand action ) {

			action.SetIdentity( User );

			var result = await _dispatcher.SendAsync( action );

			return result.IsSuccess
				? (ActionResult)NoContent()
				: UnprocessableEntity( result );
		}


	}
}
