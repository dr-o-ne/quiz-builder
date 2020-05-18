using System;
using System.Collections.Immutable;
using Newtonsoft.Json;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.ActionResult.Dto;
using QuizBuilder.Domain.ActionResult.ViewModel;

namespace QuizBuilder.Domain.Action {
	public sealed class GetQuestionsByParentQuery : IQuery<ImmutableList<QuestionViewModel>> {
		public string QuizUId { get; set; }
		public string GroupUId { get; set; }
	}

	public sealed class GetQuestionsByQuizIdQuery : IQuery<GetQuestionsByQuizIdDto> {
		public string QuizUId { get; set; }
	}

	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public string UId { get; set; }
	}
}
