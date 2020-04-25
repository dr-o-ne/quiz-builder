using System;
using QuizBuilder.Model.Model;
using QuizBuilder.Model.Model.Default.Questions;
using QuizBuilder.Repository.Dto;

namespace QuizBuilder.Model.Mapper.Default {

	public sealed class QuestionMapper : IQuestionMapper {

		public QuestionDto Map( Question entity ) {

			if( entity == null )
				return null;

			return new QuestionDto {Id = entity.Id, Name = entity.Name};
		}

		public Question Map( QuestionDto dto ) {

			if( dto == null )
				return null;

			var questionType = (Enums.QuestionType)dto.QuestionTypeId;

			Question entity = questionType switch {
				Enums.QuestionType.TrueFalse => new TrueFalseQuestion(),
				Enums.QuestionType.MultiChoice => new MultipleChoiceQuestion(),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Id = dto.Id;
			entity.Name = dto.Name;

			return entity;
		}

	}

}
