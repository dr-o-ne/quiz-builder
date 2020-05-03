using System;
using AutoMapper;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default
{
	internal class UpdateQuestionCommandToQuestionConverter : ITypeConverter<UpdateQuestionCommand, Question> {
		public Question Convert( UpdateQuestionCommand source, Question destination, ResolutionContext context ) {
			if( source is null )
				return null;

			Question entity = source.Type switch
			{
				TrueFalse => new TrueFalseQuestion(),
				MultiChoice => new MultipleChoiceQuestion(),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Name = source.Name;
			entity.Text = source.Text;

			return entity;
		}
	}
}
