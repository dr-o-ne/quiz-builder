using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default
{
	internal class QuestionToQuestionDtoConverter : ITypeConverter<Question, QuestionDto> {
		public QuestionDto Convert( Question source, QuestionDto destination, ResolutionContext context ) {
			if( source == null )
				return null;

			string settings;
			Enums.QuestionType questionType;

			switch( source ) {
				case TrueFalseQuestion question:
					questionType = TrueFalse;
					settings = JsonSerializer.Serialize( question );
					break;
				case MultipleChoiceQuestion question:
					settings = JsonSerializer.Serialize( question );
					questionType = MultiChoice;
					break;
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			return new QuestionDto {
				Id = source.Id,
				QuestionTypeId = (int)questionType,
				Name = source.Name,
				QuestionText = source.Text,
				Settings = settings
			};
		}
	}
}
