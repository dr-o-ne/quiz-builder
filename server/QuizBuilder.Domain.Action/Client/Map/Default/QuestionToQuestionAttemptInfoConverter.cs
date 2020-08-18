using System.Collections.Generic;
using System.Collections.Immutable;
using AutoMapper;
using QuizBuilder.Common.Utils;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Action.Client.Map.Default {

	internal sealed class QuestionToQuestionAttemptInfoConverter : ITypeConverter<Question, QuestionAttemptInfo> {

		public QuestionAttemptInfo Convert( Question source, QuestionAttemptInfo destination, ResolutionContext context ) {

			long GetChoicesDisplayType() {
				return source switch {
					TrueFalseQuestion trueFalseQuestion => (long)trueFalseQuestion.ChoicesDisplayType,
					MultipleChoiceQuestion multipleChoiceQuestion => (long)multipleChoiceQuestion.ChoicesDisplayType,
					MultipleSelectQuestion multipleSelectQuestion => (long)multipleSelectQuestion.ChoicesDisplayType,
					_ => 0
				};
			}

			//TODO: save isHtml in db
			(string content, bool isHtml) = QuillEditorHelper.NormalizeText( source.Text );

			return new QuestionAttemptInfo {
				UId = source.UId,
				Type = (long)source.Type,
				Text = content,
				IsHtmlText = isHtml,
				ChoicesDisplayType = GetChoicesDisplayType(),
				Choices = context.Mapper.Map<List<ChoiceAttemptInfo>>( source ).ToImmutableArray()
			};

		}
	}

}
