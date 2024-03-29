﻿using AutoMapper;
using FluentAssertions;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Action.Admin.Map;
using QuizBuilder.Domain.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Unit.Model.Questions {

	public sealed class TrueFalseChoicesQuestionTests {

		private readonly IMapper _mapper = new Mapper( new MapperConfiguration( cfg => cfg.AddProfile<MapperProfile>() ) );

		[Fact]
		public void Serialize_Deserialize_Test() {

			var expected = new TrueFalseQuestion {
				Text = "TrueFalse",
				Name = "Question Text",
				FalseChoice = {
					Text = "FalseCorrect",
					IsCorrect = true
				},
				TrueChoice = {
					Text = "TrueIncorrect",
					IsCorrect = false
				},
			};

			var dto = _mapper.Map<Question, QuestionDto>( expected );
			var actual = (TrueFalseQuestion)_mapper.Map<QuestionDto, Question>( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

	}
}
