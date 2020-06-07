using AutoMapper;
using FluentAssertions;
using QuizBuilder.Data.Dto;
using QuizBuilder.Domain.Mapper;
using QuizBuilder.Domain.Model.Default.Choices;
using QuizBuilder.Domain.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Unit.Questions {

	public sealed class MultipleChoicesQuestionTests {

		private readonly IMapper _mapper = new Mapper( new MapperConfiguration( cfg => cfg.AddProfile<QuizBuilderProfile>() ) );

		[Fact]
		public void Serialize_Deserialize_Test() {

			var expected = new MultipleChoiceQuestion {
				Text = "MultipleChoice",
				Name = "Question Text",
				Randomize = true,
			};

			expected.AddChoice( new BinaryChoice { IsCorrect = true, Text = "Choice1" } );
			expected.AddChoice( new BinaryChoice { IsCorrect = false, Text = "Choice2" } );
			expected.AddChoice( new BinaryChoice { IsCorrect = false, Text = "Choice3" } );

			var dto = _mapper.Map<Question, QuestionDto>( expected );
			var actual = (MultipleChoiceQuestion)_mapper.Map<QuestionDto, Question>( dto );

			actual.Should().BeEquivalentTo(
				expected,
				config => config
					.WithStrictOrdering()
					.IncludingAllRuntimeProperties()
			);

		}

		[Fact]
		public void Text_Validation_Test() {
			var sut = new MultipleChoiceQuestion();

			Assert.False( sut.IsValid() );
		}

		[Fact]
		public void Choices_Validation_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };

			Assert.False( sut.IsValid() );
		}

		[Fact]
		public void AllFalse_Validation_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { Text = "1", IsCorrect = false } );

			Assert.False( sut.IsValid() );
		}

		[Fact]
		public void MultipleTrue_Validation_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { Text = "1", IsCorrect = true } );
			sut.AddChoice( new BinaryChoice { Text = "2", IsCorrect = true } );

			Assert.False( sut.IsValid() );
		}

		[Fact]
		public void ChoiceText_Validation_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { IsCorrect = true } );

			Assert.False( sut.IsValid() );
		}

		[Fact]
		public void Success_Validation_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { Text = "1", IsCorrect = true } );
			sut.AddChoice( new BinaryChoice { Text = "2", IsCorrect = false } );

			Assert.True( sut.IsValid() );
		}

		[Fact]
		public void Randomize_False_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { Text = 0.ToString(), IsCorrect = true } );
			for( int i = 1; i < 100; i++ )
				sut.AddChoice( new BinaryChoice { Text = i.ToString(), IsCorrect = false } );

			Assert.True( sut.IsValid() );

			var choices = sut.GetChoicesRandomized();
			Assert.True( choices[0].IsCorrect );
			for( int i = 1; i < 100; i++ )
				Assert.False( choices[i].IsCorrect );
		}

		[Fact]
		public void Randomize_True_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question", Randomize = true };
			sut.AddChoice( new BinaryChoice { Text = 0.ToString(), IsCorrect = true } );
			for( int i = 1; i < 10000; i++ )
				sut.AddChoice( new BinaryChoice { Text = i.ToString(), IsCorrect = false } );

			Assert.False( sut.GetChoicesRandomized()[0].IsCorrect );
		}

	}

}
