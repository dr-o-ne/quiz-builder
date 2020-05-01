using AutoMapper;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class QuestionToQuestionViewModelConverter: ITypeConverter<Question, QuestionViewModel> {
		public QuestionViewModel Convert( Question source, QuestionViewModel destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new QuestionViewModel {
				Id = source.Id,
				Name = source.Name
			};
		}
	}
}
