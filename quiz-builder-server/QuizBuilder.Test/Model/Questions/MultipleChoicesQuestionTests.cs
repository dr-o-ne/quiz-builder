using QuizBuilder.Model.Model.Default.Choices;
using QuizBuilder.Model.Model.Default.Questions;
using Xunit;

namespace QuizBuilder.Test.Model.Questions {

	public sealed class MultipleChoicesQuestionTests {

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

			Assert.True( sut.IsValid() );
		}

		[Fact]
		public void Randomize_False_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question" };
			sut.AddChoice( new BinaryChoice { Text = 0.ToString(), IsCorrect = true } );
			for( int i = 1; i < 100; i++ ) 
				sut.AddChoice( new BinaryChoice { Text = i.ToString(), IsCorrect = false } );

			Assert.True( sut.IsValid() );
			Assert.True( sut.Choices[0].IsCorrect );
			for( int i = 1; i < 100; i++ ) 
				Assert.False( sut.Choices[i].IsCorrect );
		}

		[Fact]
		public void Randomize_True_Test() {
			var sut = new MultipleChoiceQuestion { Text = "Test Question", Randomize = true };
			sut.AddChoice( new BinaryChoice { Text = 0.ToString(), IsCorrect = true } );
			for( int i = 1; i < 10000; i++ )
				sut.AddChoice( new BinaryChoice { Text = i.ToString(), IsCorrect = false } );

			Assert.False( sut.Choices[0].IsCorrect );
		}

	}

}
