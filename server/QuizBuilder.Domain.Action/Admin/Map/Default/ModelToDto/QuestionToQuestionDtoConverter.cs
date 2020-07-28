using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToDto {
	internal sealed class QuestionToQuestionDtoConverter : ITypeConverter<Question, QuestionDto> {
		public QuestionDto Convert( Question source, QuestionDto destination, ResolutionContext context ) {
			if( source == null )
				return null;

			string settings;
			Enums.QuizItemType questionType;

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
				case LongAnswerQuestion question:
					settings = JsonSerializer.Serialize( question );
					questionType = question.Type;
					break;
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			return new QuestionDto {
				Id = source.Id,
				UId = source.UId,
				TypeId = (int)questionType,
				Name = source.Name,
				Text = source.Text,
				Settings = settings
			};
		}
	}
}
