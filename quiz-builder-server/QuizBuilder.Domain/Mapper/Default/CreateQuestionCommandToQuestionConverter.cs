using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default {
	internal class CreateQuestionCommandToQuestionConverter : ITypeConverter<CreateQuestionCommand, Question> {
		public Question Convert( CreateQuestionCommand source, Question destination, ResolutionContext context ) {
			if( source is null )
				return null;

			Question entity = source.QuestionType switch
			{
				TrueFalse => JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings ),
				MultiChoice => JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings ),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Name = source.Name;
			entity.Text = source.QuestionText;

			return entity;
		}
	}
}
