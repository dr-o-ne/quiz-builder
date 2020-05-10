using AutoMapper;
using QuizBuilder.Domain.Commands;
using QuizBuilder.Domain.Commands.QuizCommands;
using QuizBuilder.Domain.Model.Default;

namespace QuizBuilder.Domain.Mapper.Default
{
	public class UpdateQuizCommandToQuizConverter: ITypeConverter<UpdateQuizCommand, Quiz> {
		public Quiz Convert( UpdateQuizCommand source, Quiz destination, ResolutionContext context ) {
			if( source is null )
				return null;

			return new Quiz {
				UId = source.Id,
				IsVisible = source.IsVisible,
				Name = source.Name
			};
		}
	}
}
