using System;
using System.Collections.Immutable;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Actions {
	public sealed class GetQuestionsByParentQuery : IQuery<ImmutableList<QuestionViewModel>> {
		public string QuizUId { get; set; }
		public string GroupUId { get; set; }
	}

	public sealed class GetQuestionsByGroupIdQuery : IQuery<GetQuestionsByGroupIdDto> {
		public Guid GroupId { get; set; }
	}

	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public string UId { get; set; }
	}
}
