using AutoMapper;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class UpdateQuizCommandToQuizConverter: ITypeConverter<UpdateQuizCommand, Quiz> {
		public Quiz Convert( UpdateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {
				Id = source.Id,
				Name = source.Name
			};
		}
	}
}
