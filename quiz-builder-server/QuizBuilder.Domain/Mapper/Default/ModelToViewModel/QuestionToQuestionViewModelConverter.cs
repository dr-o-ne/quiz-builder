using System;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using QuizBuilder.Domain.Model.View;

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
					settings = JsonConvert.SerializeObject( settingsMultiChoice, SetJsonOptions() );
					choices = JsonConvert.SerializeObject( question.Choices, SetJsonOptions() );
					break;
				case MultipleSelectQuestion question:
					var settingsMultipleSelect = new {
						ChoicesDisplayType = question.ChoicesDisplayType,
						ChoicesEnumerationType = question.ChoicesEnumerationType,
						GradingType = question.GradingType,
						Randomize = question.Randomize
					};
					settings = JsonConvert.SerializeObject( settingsMultipleSelect, SetJsonOptions() );
					choices = JsonConvert.SerializeObject( question.Choices, SetJsonOptions() );
					break;
				case TrueFalseQuestion question:
					var settingsTrueFalse = new {
						ChoicesDisplayType = question.ChoicesDisplayType,
						ChoicesEnumerationType = question.ChoicesEnumerationType
					};
					settings = JsonConvert.SerializeObject( settingsTrueFalse, SetJsonOptions() );
					var binaryChoices = new BinaryChoice[] { question.TrueChoice, question.FalseChoice };
					choices = JsonConvert.SerializeObject( binaryChoices, SetJsonOptions() );
					break;
				case LongAnswerQuestion question:
					var settingsLongAnswer = new {
						AnswerText = question.AnswerText
					};
					settings = JsonConvert.SerializeObject( settingsLongAnswer, SetJsonOptions() );
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

		private JsonSerializerSettings SetJsonOptions() {
			DefaultContractResolver contractResolver = new DefaultContractResolver {
				NamingStrategy = new CamelCaseNamingStrategy()
			};
			return new JsonSerializerSettings {
				ContractResolver = contractResolver,
				Formatting = Formatting.Indented
			};
		}
	}
}
