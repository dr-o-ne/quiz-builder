using AutoMapper;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;

namespace QuizBuilder.Domain.Mapper.Default
{
	internal class CreateQuestionCommandToQuestionConverter : ITypeConverter<CreateQuestionCommand, Question> {
		public Question Convert( CreateQuestionCommand source, Question destination, ResolutionContext context ) {
			throw new System.NotImplementedException();
		}
	}
}