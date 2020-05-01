using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default
{
	internal class QuestionDtoToQuestionConverter : ITypeConverter<QuestionDto, Question> {
		public Question Convert( QuestionDto source, Question destination, ResolutionContext context ) {
			if( source == null )
				return null;

			var questionType = (Enums.QuestionType)source.QuestionTypeId;

			Question entity = questionType switch {
				TrueFalse => JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings ),
				MultiChoice => JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings ),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Id = source.Id;
			entity.Name = source.Name;
			entity.Text = source.QuestionText;

			return entity;
		}
	}
}
