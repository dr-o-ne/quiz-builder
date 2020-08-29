using System;
using AutoMapper;
using QuizBuilder.Common;
using QuizBuilder.Domain.Action.Admin.ActionResult.ViewModel;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ModelToViewModel {
	internal sealed class QuestionToQuestionViewModelConverter: ITypeConverter<Question, QuestionViewModel> {

		public QuestionViewModel Convert( Question source, QuestionViewModel destination, ResolutionContext context ) {

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
				case LongAnswerQuestion _:
					settings = JsonSerializer.Serialize( new {}, Consts.JsonSerializerOptions );
					choices = string.Empty;
					break;
				case EmptyQuestion _:
					settings = JsonSerializer.Serialize( new { }, Consts.JsonSerializerOptions );
					choices = string.Empty;
					break;
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			return new QuestionViewModel {
				Id = source.UId,
				GroupId = source.ParentUId,
				Type = source.Type,
				Name = source.Name,
				Text = source.Text,
				Points = source.Points,
				IncorrectFeedback = source.IncorrectFeedback,
				CorrectFeedback = source.CorrectFeedback,
				Settings = settings,
				Choices = choices,
				IsRequired = source.IsRequired
			};
		}

	}
}
