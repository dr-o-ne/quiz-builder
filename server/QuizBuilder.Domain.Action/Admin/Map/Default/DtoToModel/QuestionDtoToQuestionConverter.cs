using System;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Model.Default;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.DtoToModel {

	internal sealed class QuestionDtoToQuestionConverter : ITypeConverter<QuestionDto, Question> {

		public Question Convert( QuestionDto source, Question destination, ResolutionContext context ) {

			var questionType = (Enums.QuizItemType)source.TypeId;

			Question result = questionType switch {
				TrueFalse => JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings ),
				MultiChoice => JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings ),
				FillInTheBlanks => JsonSerializer.Deserialize<FillInTheBlanksQuestion>( source.Settings ),
				MultiSelect => JsonSerializer.Deserialize<MultipleSelectQuestion>( source.Settings ),
				LongAnswer => JsonSerializer.Deserialize<LongAnswerQuestion>( source.Settings ),
				_ => throw new ArgumentException( "Unknown question type" )
			};

			result.Id = source.Id;
			result.UId = source.UId;
			result.ParentUId = source.ParentUId;
			result.Name = source.Name;
			result.Text = source.Text;
			result.SortOrder = source.SortOrder;

			return result;
		}
	}
}
