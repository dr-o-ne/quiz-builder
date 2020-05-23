using System;
using AutoMapper;
using QuizBuilder.Domain.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuizBuilder.Domain.Mapper.Default.ModelToViewModel {
	internal sealed class QuestionToQuestionViewModelConverter: ITypeConverter<Question, QuestionViewModel> {
		public QuestionViewModel Convert( Question source, QuestionViewModel destination, ResolutionContext context ) {
			if( source is null )
				return null;

			string settings;
			string choices;
			switch( source ) {
				case MultipleChoiceQuestion question:
					var settingsMultiChoice = new {
						ChoicesDisplayType = question.ChoicesDisplayType,
						ChoicesEnumerationType = question.ChoicesEnumerationType,
						Randomize = question.Randomize
					};
					settings = JsonSerializer.Serialize( settingsMultiChoice, Consts.JsonSerializerOptions );
					choices = JsonSerializer.Serialize( question.Choices, Consts.JsonSerializerOptions );
					break;
				case MultipleSelectQuestion question:
					var settingsMultipleSelect = new {
						ChoicesDisplayType = question.ChoicesDisplayType,
						ChoicesEnumerationType = question.ChoicesEnumerationType,
						GradingType = question.GradingType,
						Randomize = question.Randomize
					};
					settings = JsonSerializer.Serialize( settingsMultipleSelect, Consts.JsonSerializerOptions );
					choices = JsonSerializer.Serialize( question.Choices, Consts.JsonSerializerOptions );
					break;
				case TrueFalseQuestion question:
					var settingsTrueFalse = new {
						ChoicesDisplayType = question.ChoicesDisplayType,
						ChoicesEnumerationType = question.ChoicesEnumerationType
					};
					settings = JsonSerializer.Serialize( settingsTrueFalse, Consts.JsonSerializerOptions );
					var binaryChoices = new BinaryChoice[] { question.TrueChoice, question.FalseChoice };
					choices = JsonSerializer.Serialize( binaryChoices, Consts.JsonSerializerOptions );
					break;
				case LongAnswerQuestion question:
					var settingsLongAnswer = new {
						AnswerText = question.AnswerText
					};
					settings = JsonSerializer.Serialize( settingsLongAnswer, Consts.JsonSerializerOptions );
					choices = string.Empty;
					break;
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			return new QuestionViewModel {
				Id = source.UId,
				Type = source.Type,
				Name = source.Name,
				Text = source.Text,
				Feedback = source.Feedback,
				IncorrectFeedback = source.IncorrectFeedback,
				CorrectFeedback = source.CorrectFeedback,
				Settings = settings,
				Choices = choices
			};
		}

	}
}
