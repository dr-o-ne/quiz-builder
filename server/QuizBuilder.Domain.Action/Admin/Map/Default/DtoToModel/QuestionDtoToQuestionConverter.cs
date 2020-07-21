using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuestionType;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.DtoToModel {

	internal sealed class QuestionDtoToQuestionConverter : ITypeConverter<QuestionDto, Question> {
		public Question Convert( QuestionDto source, Question destination, ResolutionContext context ) {

			var questionType = (Enums.QuestionType)source.TypeId;

			Question entity = questionType switch {
				TrueFalse => JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings ),
				MultiChoice => JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings ),
				FillInTheBlanks => JsonSerializer.Deserialize<FillInTheBlanksQuestion>( source.Settings ),
				MultiSelect => JsonSerializer.Deserialize<MultipleSelectQuestion>( source.Settings ),
				LongAnswer => JsonSerializer.Deserialize<LongAnswerQuestion>( source.Settings ),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			entity.Id = source.Id;
			entity.UId = source.UId;
			entity.ParentUId = source.ParentUId;
			entity.Name = source.Name;
			entity.Text = source.Text;
			entity.Points = source.Points;
			entity.SortOrder = source.SortOrder;

			return entity;
		}
	}
}
