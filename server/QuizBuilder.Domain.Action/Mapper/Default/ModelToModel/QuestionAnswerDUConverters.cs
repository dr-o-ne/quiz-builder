﻿using AutoMapper;
using QuizBuilder.Domain.Model.Default.Answers;
using QuizBuilder.Domain.Model.Default.Attempts;

namespace QuizBuilder.Domain.Action.Mapper.Default.ModelToModel {

	internal sealed class QuestionAnswerDUToMultipleSelectAnswer : ITypeConverter<QuestionAnswerDU, MultipleSelectAnswer> {

		public MultipleSelectAnswer Convert( QuestionAnswerDU source, MultipleSelectAnswer destination, ResolutionContext context ) =>
			new MultipleSelectAnswer { QuestionUId = source.QuestionUId, ChoiceSelections = source.BinaryChoiceSelection };

	}
}
