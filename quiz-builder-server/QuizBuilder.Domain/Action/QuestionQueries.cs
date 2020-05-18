using System;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.ActionResult;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action {

	public sealed class GetQuestionsByParentQuery : IQuery<ImmutableList<QuestionViewModel>> {

		[FromQuery( Name = "quizId" )]
		public string QuizUId { get; set; }

		[FromQuery( Name = "groupId" )]
		public string GroupUId { get; set; }
	}

	public sealed class GetQuestionsByGroupIdQuery : IQuery<QuestionsQueryResult> {
		public Guid GroupId { get; set; }
	}

	public sealed class GetQuestionByIdQuery : IQuery<QuestionQueryResult> {
		public string UId { get; set; }
	}
}
