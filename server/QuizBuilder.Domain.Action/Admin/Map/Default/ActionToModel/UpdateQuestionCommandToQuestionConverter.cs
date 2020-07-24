using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Action.Admin.Action;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Default.Enums.QuestionType;

namespace QuizBuilder.Domain.Action.Admin.Map.Default.ActionToModel {

	internal sealed class UpdateQuestionCommandToQuestionConverter : ITypeConverter<UpdateQuestionCommand, Question> {

		public Question Convert( UpdateQuestionCommand source, Question destination, ResolutionContext context ) {
			if( source is null )
				return null;

			Question question;
			var serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

			switch( source.Type ) {
				case TrueFalse: {
						var entity = JsonSerializer.Deserialize<TrueFalseQuestion>( source.Settings, serializerOptions );
						var choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, serializerOptions );
						entity.TrueChoice = choices.FirstOrDefault();
						entity.FalseChoice = choices.LastOrDefault();
						question = entity;
						break;
					}
				case MultiChoice: {
						var entity = JsonSerializer.Deserialize<MultipleChoiceQuestion>( source.Settings, serializerOptions );
						entity.Choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, serializerOptions );
						question = entity;
						break;
					}
				case MultiSelect: {
						var entity = JsonSerializer.Deserialize<MultipleSelectQuestion>( source.Settings, serializerOptions );
						entity.Choices = JsonSerializer.Deserialize<List<BinaryChoice>>( source.Choices, serializerOptions );
						question = entity;
						break;
					}
				case LongAnswer: {
						question = JsonSerializer.Deserialize<LongAnswerQuestion>( source.Settings, serializerOptions );
						break;
					}
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			question.UId = source.UId;
			question.Name = source.Name;
			question.Text = source.Text;
			question.Points = source.Points;
			question.Feedback = source.Feedback;
			question.CorrectFeedback = source.CorrectFeedback;
			question.IncorrectFeedback = source.IncorrectFeedback;

			return question;
		}
	}
}