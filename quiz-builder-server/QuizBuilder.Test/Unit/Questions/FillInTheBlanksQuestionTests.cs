using AutoMapper;
using FluentAssertions;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Unit.Questions {

	public sealed class FillInTheBlanksQuestionTests {

		private readonly IMapper _mapper = new Mapper( new MapperConfiguration( cfg => cfg.AddProfile<QuizBuilderProfile>() ) );

		[Fact]
		public void Serialize_Deserialize_Test() {

			var expected = new FillInTheBlanksQuestion {
				Name = "Fill In The Blanks Question Sample"
			};

			expected.Texts.Add( new FillInTheBlanksText {Order = 0, Text = "<p>The oceanic crust is made of "} );
			expected.Texts.Add( new FillInTheBlanksText {Order = 1, Text = " and "} );
			expected.Texts.Add( new FillInTheBlanksText {Order = 2, Text = " and is "} );
			expected.Texts.Add( new FillInTheBlanksText {Order = 3, Text = " "} );
			expected.Texts.Add( new FillInTheBlanksText {Order = 4, Text = " thick.</p>"} );

			var group0 = new TextChoiceGroup {Order = 0};
			group0.Choices.Add( new TextChoice {Text = "bassalt", TextEvaluationType = Enums.TextEvaluationType.CaseInsensitive} );
			expected.ChoiceGroups.Add( group0 );

			var group1 = new TextChoiceGroup {Order = 1};
			group1.Choices.Add( new TextChoice {Text = "gabbro", TextEvaluationType = Enums.TextEvaluationType.CaseInsensitive} );
			expected.ChoiceGroups.Add( group1 );

			var group2 = new TextChoiceGroup { Order = 2 };
			group2.Choices.Add( new TextChoice { Text = "5", TextEvaluationType = Enums.TextEvaluationType.RegularExpression} );
			expected.ChoiceGroups.Add( group2 );

			var group3 = new TextChoiceGroup { Order = 3 };
			group3.Choices.Add( new TextChoice { Text = "km", TextEvaluationType = Enums.TextEvaluationType.RegularExpression } );
			group3.Choices.Add( new TextChoice { Text = "kilometers", TextEvaluationType = Enums.TextEvaluationType.CaseInsensitive } );
			expected.ChoiceGroups.Add( group3 );

			var dto = _mapper.Map<Question, QuestionDto>( expected );
			var actual = (FillInTheBlanksQuestion)_mapper.Map<QuestionDto, Question>( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

	}

}
