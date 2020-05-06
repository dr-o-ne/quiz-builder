﻿using System;
using QuizBuilder.Common.Types;
using QuizBuilder.Common.Types.Default;
using QuizBuilder.Domain.Model;

namespace QuizBuilder.Domain.Commands.QuestionCommands {
	public class UpdateQuestionCommand : ICommand<CommandResult> {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public long GroupId { get; set; }
		public Enums.QuestionType Type { get; set; }
		public string Text { get; set; }
		public string Feedback { get; set; }
		public string CorrectFeedback { get; set; }
		public string IncorrectFeedback { get; set; }
		public string Settings { get; set; }
		public string Choices { get; set; }
	}
}
