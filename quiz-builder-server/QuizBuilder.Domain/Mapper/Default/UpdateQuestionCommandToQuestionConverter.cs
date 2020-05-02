using AutoMapper;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Mapper.Default
{
	internal class UpdateQuestionCommandToQuestionConverter : ITypeConverter<UpdateQuestionCommand, Question> {
		public Question Convert( UpdateQuestionCommand source, Question destination, ResolutionContext context ) {
			throw new System.NotImplementedException();
		}
	}
}