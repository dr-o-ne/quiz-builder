using System.Collections.Generic;
using AutoMapper;
using QuizBuilder.Domain.Action.Action;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Attempts;

namespace QuizBuilder.Domain.Action.Mapper.Default.ActionToModel {

	public sealed class EndQuizAttemptCommand2ListOfQuestionAnswerDU : ITypeConverter<EndQuizAttemptCommand, List<QuestionAnswerDU>> {

		public List<QuestionAnswerDU> Convert( EndQuizAttemptCommand source, List<QuestionAnswerDU> destination, ResolutionContext context ) {

			var result = new List<QuestionAnswerDU>(source.QuestionAnswers.Count);

			foreach( var item in source.QuestionAnswers ) {

				result.Add( new QuestionAnswerDU {
					QuestionUId = item.QuestionUId,
					BinaryChoiceSelection = item.BinaryChoiceSelections,
					//TODO: text answers?
				} );

			}

			return result;


		}

	}

}
