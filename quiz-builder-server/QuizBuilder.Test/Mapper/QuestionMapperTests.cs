using FluentAssertions;
using QuizBuilder.Model.Mapper;
using QuizBuilder.Model.Mapper.Default;
using QuizBuilder.Model.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Mapper {

	public sealed class QuestionMapperTests {

		private readonly IQuestionMapper _sut = new QuestionMapper();

		[Fact]
		public void TrueFalse_Serialize_Deserialize_Test() {

			var entity = new TrueFalseQuestion {
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

			var dto = _sut.Map( entity );
			var actual = (TrueFalseQuestion)_sut.Map( dto );

			actual.Should().BeEquivalentTo(
				entity,
				opt => opt
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

	}

}
