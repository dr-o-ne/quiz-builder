using FluentAssertions;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Mapper.Default;
using QuizBuilder.Model.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Unit.Questions {

	public sealed class TrueFalseChoicesQuestionTests {

		private readonly IQuestionMapper _mapper = new QuestionMapper();

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
				}
			};

			var dto = _mapper.Map( expected );
			var actual = (TrueFalseQuestion)_mapper.Map( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

	}
}
