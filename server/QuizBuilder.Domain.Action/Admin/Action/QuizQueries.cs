﻿using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Action.Admin.ActionResult;

namespace QuizBuilder.Domain.Action.Admin.Action {

	public sealed class GetAllQuizzesQuery : IQuery<QuizzesQueryResult> {
	}

	public sealed class GetQuizByIdQuery : IQuery<QuizQueryResult> {

		public string UId { get; set; }

	}

}