using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Domain.Dtos;

namespace QuizBuilder.Domain.Actions {

	public sealed class GetAllQuestionQuery : IQuery<GetAllQuestionsDto> {
	}

	public sealed class GetQuestionsByGroupIdQuery : IQuery<GetQuestionsByGroupIdDto> {
		public Guid GroupId { get; set; }
	}

	public sealed class GetQuestionByIdQuery : IQuery<GetQuestionByIdDto> {
		public string UId { get; set; }
	}

}
