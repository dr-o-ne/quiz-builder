using FluentAssertions;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Mapper.Default;
using QuizBuilder.Model.Model.Default.Choices;
using QuizBuilder.Model.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Mapper {

	public sealed class QuestionMapperTests {

		private readonly IQuestionMapper _sut = new QuestionMapper();

		[Fact]
		public void TrueFalse_Serialize_Deserialize_Test() {

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
				}
			};

			var dto = _sut.Map( expected );
			var actual = (TrueFalseQuestion)_sut.Map( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

		[Fact]
		public void MultipleChoice_Serialize_Deserialize_Test() {

			var expected = new MultipleChoiceQuestion {
				Text = "MultipleChoice",
				Name = "Question Text",
				Randomize = true
			};

			expected.AddChoice( new BinaryChoice {IsCorrect = true, Text = "Choice1"} );
			expected.AddChoice( new BinaryChoice {IsCorrect = false, Text = "Choice2"} );
			expected.AddChoice( new BinaryChoice {IsCorrect = false, Text = "Choice3"} );

			var dto = _sut.Map( expected );
			var actual = (MultipleChoiceQuestion)_sut.Map( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

	}

}
