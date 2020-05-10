using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using QuizBuilder.Domain.Commands.QuestionCommands;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using static QuizBuilder.Domain.Model.Enums.QuestionType;

namespace QuizBuilder.Domain.Mapper.Default {

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
				default:
					throw new ArgumentException( "Unknown question type" );
			}

			question.UId = source.Id;
			question.Name = source.Name;
			question.Text = source.Text;
			question.Feedback = source.Feedback;
			question.CorrectFeedback = source.CorrectFeedback;
			question.IncorrectFeedback = source.IncorrectFeedback;

			return question;
		}
	}
}
