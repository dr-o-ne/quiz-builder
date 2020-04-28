using System;
using System.Text.Json;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Repository.Dto;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default {

	public sealed class QuestionMapper : IQuestionMapper {

		public QuestionDto Map( Question entity ) {

			if( entity == null )
				return null;

			string settings;
			Enums.QuestionType questionType;

			switch( entity ) {
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
				Id = entity.Id,
				QuestionTypeId = (int)questionType,
				Name = entity.Name,
				QuestionText = entity.Text,
				Settings = settings
			};
		}

		public Question Map( QuestionDto dto ) {

			if( dto == null )
				return null;

			var questionType = (Enums.QuestionType)dto.QuestionTypeId;

			Question entity = questionType switch {
				TrueFalse => JsonSerializer.Deserialize<TrueFalseQuestion>( dto.Settings ),
				MultiChoice => JsonSerializer.Deserialize<MultipleChoiceQuestion>( dto.Settings ),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Id = dto.Id;
			entity.Name = dto.Name;
			entity.Text = dto.QuestionText;

			return entity;
		}

	}

}
