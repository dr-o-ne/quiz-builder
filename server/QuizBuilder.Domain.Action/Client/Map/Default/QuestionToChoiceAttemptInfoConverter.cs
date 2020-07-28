using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using QuizBuilder.Domain.Action.Client.ActionResult;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Client.Map.Default {

	internal sealed class QuestionToChoiceAttemptInfoConverter : ITypeConverter<Question, List<ChoiceAttemptInfo>> {

		public List<ChoiceAttemptInfo> Convert( Question source, List<ChoiceAttemptInfo> destination, ResolutionContext context ) {

			switch( source.Type ) {
				case TrueFalse: {
					var x = (TrueFalseQuestion)source;
					return new List<ChoiceAttemptInfo> {
						context.Mapper.Map<ChoiceAttemptInfo>( x.TrueChoice ),
						context.Mapper.Map<ChoiceAttemptInfo>( x.FalseChoice )
					};
				}
				case MultiChoice: {
					var x = (MultipleChoiceQuestion)source;
					return x.GetChoicesRandomized().Select( context.Mapper.Map<ChoiceAttemptInfo> ).ToList();
				}
				case MultiSelect: {
					var x = (MultipleSelectQuestion)source;
					return x.GetChoicesRandomized().Select( context.Mapper.Map<ChoiceAttemptInfo> ).ToList();
				}
				case LongAnswer:
					return Enumerable.Empty<ChoiceAttemptInfo>().ToList();
				default:
					throw null;
			}

		}

	}
}
