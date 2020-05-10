using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default {
	internal sealed class QuestionToQuestionDtoConverter : ITypeConverter<Question, QuestionDto> {
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
				case FillInTheBlanksQuestion question:
					settings = JsonSerializer.Serialize( question );
					questionType = FillInTheBlanks;
					break;
				case MultipleSelectQuestion question:
					settings = JsonSerializer.Serialize( question );
					questionType = question.Type;
					break;
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			return new QuestionDto {
				Id = source.Id,
				UId = source.UId,
				QuestionTypeId = (int)questionType,
				Name = source.Name,
				QuestionText = source.Text,
				Settings = settings
			};
		}
	}
}
