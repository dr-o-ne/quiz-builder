using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Common;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuizItemType;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ActionToModel {
	internal sealed class CreateQuestionCommandToQuestionConverter : ITypeConverter<CreateQuestionCommand, Question> {

		public Question Convert( CreateQuestionCommand source, Question destination, ResolutionContext context ) {

			Question question;
			switch( source.Type ) {
				case TrueFalse: {
					var entity = JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings, Consts.JsonSerializerOptions );
					var choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, Consts.JsonSerializerOptions );
					entity.TrueChoice = choices.FirstOrDefault();
					entity.FalseChoice = choices.LastOrDefault();
					question = entity;
					break;
				}
				case MultiChoice: {
					var entity = JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings, Consts.JsonSerializerOptions );
					entity.Choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, Consts.JsonSerializerOptions );
					question = entity;
					break;
				}
				case MultiSelect: {
					var entity = JsonSerializer.Deserialize<MultipleSelectQuestion>( source.Settings, Consts.JsonSerializerOptions );
					entity.Choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, Consts.JsonSerializerOptions );
					question = entity;
					break;
				}
				case LongAnswer: {
					question = JsonSerializer.Deserialize<LongAnswerQuestion>( source.Settings, Consts.JsonSerializerOptions );
					break;
				}
				case Empty: {
					question = JsonSerializer.Deserialize<EmptyQuestion>( source.Settings, Consts.JsonSerializerOptions );
					break;
				}
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			question.Name = source.Name;
			question.Text = source.Text;
			question.Points = source.Points;
			question.CorrectFeedback = source.CorrectFeedback;
			question.IncorrectFeedback = source.IncorrectFeedback;
			question.IsRequired = source.IsRequired;
			question.TimeLimit = source.TimeLimit;

			return question;
		}
	}
}
